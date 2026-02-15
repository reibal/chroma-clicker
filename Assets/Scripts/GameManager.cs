using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game logic
    [SerializeField] private double currentChroma = 0f;
    private double chromaPerSecond = 0f;
    private double chromaIncreasePerClick = 1f;
    [SerializeField] private int pureChroma = 0; // <-- Used to calculate prestigeBonus

    // Public getters
    public double CurrentChroma => currentChroma;
    public int PureChroma => pureChroma;

    // Delays for loops
    private readonly float loopDelaySeconds = 0.05f; // 0.05f delay = 20 recalculations per second
    private readonly int autosaveDelaySeconds = 5;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Load from save file (it will be created if it doesn't exist)
        LoadData();
        // Coroutine loops
        StartCoroutine(ChromaPerSecondLoop());
        StartCoroutine(AutoSaveLoop());
    }

    void OnApplicationFocus()
    {
        AddAfkFarmedChroma(SaveSystem.Load().GetSecondsFromLastSession());
    }

    void OnApplicationPause()
    {
        SaveData();
    }

    private void LoadData()
    {
        // Load saved data
        SaveData saveData = SaveSystem.Load();
        // Total chroma from saved data
        currentChroma = saveData.GetCurrentChroma();
        UIManager.Instance.SetCurrentChromaUI(saveData.currentChroma);
        // Pure chroma (prestige) from saved data
        pureChroma = saveData.GetPureChroma();
        // Initialize resources with saved data
        ResourcesManager.Instance.InitializeResources(saveData.GetResourcesAmounts());
        // With the updated values, recalculate chroma per second (and click increase)
        RecalculateTotalChromaPerSecond();
        // Add chroma farmed while afk
        AddAfkFarmedChroma(saveData.GetSecondsFromLastSession());
    }

    private void SaveData()
    {
        SaveSystem.Save(currentChroma, ResourcesManager.Instance.Resources, pureChroma);
    }

    public void OnChromaClicked()
    {
        currentChroma += chromaIncreasePerClick;
        UIManager.Instance.SetCurrentChromaUI(currentChroma);
    }

    public bool SpendChroma(double amount)
    {
        if (currentChroma >= amount)
        {
            currentChroma -= amount;
            UIManager.Instance.SetCurrentChromaUI(currentChroma);
            return true;
        }
        return false;
    }

    private void AddAfkFarmedChroma(long elapsedSeconds)
    {
        // GUARD CLAUSE: Do nothing if no chroma is being generated, or if less than 2 minutes passed since last session
        if (chromaPerSecond == 0 || elapsedSeconds < 120) return;
        // Increase chroma and show message to player
        double obtainedChroma = Math.Round(chromaPerSecond * elapsedSeconds * 0.4f);
        string timeLapse = Utils.FormatTimeLapseFromSeconds(elapsedSeconds);
        UIManager.Instance.ShowInfoMessage("You were away for " + timeLapse + ". You got " + obtainedChroma + " chroma while you were AFK.");
        currentChroma += obtainedChroma;
        // Save data to prevent showing the same message again
        SaveData();
    }

    public void RecalculateTotalChromaPerSecond()
    {
        float prestigeBonus = Prestige.CalculateIncreaseFromPureChroma(pureChroma);
        chromaPerSecond = ResourcesManager.Instance.CalculateTotalChromaPerSecond() * (1 + (prestigeBonus / 100));
        UIManager.Instance.SetChromaPerSecondUI(chromaPerSecond, prestigeBonus);
        chromaIncreasePerClick = 1f + (chromaPerSecond * 0.02f); // Each click is = 1 + (2% of cps)
        ResourcesManager.Instance.RevalidatePurchaseButtonsAvailability(currentChroma);
    }

    // Running prestige will set currentChroma to 0, all resources to none, and will increase the 
    public void ActivatePrestige()
    {
        int pureChromaGained = Prestige.CalculatePureChromaFromChroma(currentChroma);
        // Increase pure chroma
        pureChroma += pureChromaGained;
        // Reset chroma and resources
        currentChroma = 0;
        UIManager.Instance.SetCurrentChromaUI(0);
        ResourcesManager.Instance.ResetResourcesList();
        // With the updated values, recalculate chroma per second (and click increase)
        RecalculateTotalChromaPerSecond();
    }

    public void OnHardResetButtonClicked()
    {
        // Reset only after confirmation
        UIManager.Instance.ShowConfirmMessage(
            "This will <b>DELETE ALL DATA</b>. This action is <b>IRREVERSIBLE</b>. Do you wish to continue?",
            () =>
            {
                SaveSystem.HardResetSavedData();
                LoadData();
            }
        );
    }

    // ------- Loops (Coroutines) -------
    // CPS loop (auto-generated currency)
    IEnumerator ChromaPerSecondLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(loopDelaySeconds);
            currentChroma += chromaPerSecond * loopDelaySeconds;
            UIManager.Instance.SetCurrentChromaUI(currentChroma);
            ResourcesManager.Instance.RevalidatePurchaseButtonsAvailability(currentChroma);
        }
    }
    // Auto-Save
    IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(autosaveDelaySeconds);
            SaveData();
        }
    }
}
