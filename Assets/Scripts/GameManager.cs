using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game logic
    [SerializeField] private float currentChroma = 0f;
    private float chromaPerSecond = 0f;
    private float chromaIncreasePerClick = 1f;

    // Public getters
    public float CurrentChroma => currentChroma;

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

    private void LoadData()
    {
        // Load saved data
        SaveData saveData = SaveSystem.Load();
        // Total chroma from saved data
        currentChroma = saveData.GetCurrentChroma();
        UIManager.Instance.SetCurrentChromaUI(saveData.currentChroma);
        // With the updated values, recalculate chroma per second (and click increase)
        RecalculateTotalChromaPerSecond();
        // Add extra afk-farmed chroma
        AddAfkFarmedChroma(saveData.GetSecondsFromLastSession());
    }

    public void OnChromaClicked()
    {
        currentChroma += chromaIncreasePerClick;
        UIManager.Instance.SetCurrentChromaUI(currentChroma);
    }

    IEnumerator ChromaPerSecondLoop()
    {
        while (true)
        {
            currentChroma += chromaPerSecond * loopDelaySeconds;
            UIManager.Instance.SetCurrentChromaUI(currentChroma);
            yield return new WaitForSeconds(loopDelaySeconds);
        }
    }

    IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(autosaveDelaySeconds);
            SaveSystem.Save(currentChroma, ResourcesManager.Instance.Resources);
        }
    }

    public bool SpendChroma(float amount)
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
        // Do nothing if less than 2 minutes passed since last session
        if (elapsedSeconds < 120) return;
        // Increase chroma and show message to player
        float obtainedChroma = Mathf.Round(chromaPerSecond * elapsedSeconds * 0.4f);
        string timeLapse = Utils.FormatTimeLapseFromSeconds(elapsedSeconds);
        UIManager.Instance.ShowMessage("You were away for " + timeLapse + ". You got " + obtainedChroma + " chroma while you were AFK.");
        currentChroma += obtainedChroma;
        // Save data to prevent showing the same message again
        SaveSystem.Save(currentChroma, ResourcesManager.Instance.Resources);
    }

    public void RecalculateTotalChromaPerSecond()
    {
        chromaPerSecond = ResourcesManager.Instance.CalculateTotalChromaPerSecond();
        UIManager.Instance.SetChromaPerSecondUI(chromaPerSecond);
        chromaIncreasePerClick = 1f + (chromaPerSecond * 0.05f); // Each click is = 1 + (5% of cps)
    }

    public void DeleteAllSavedData()
    {
        // TODO: Add a confirmation message (instead of just an info message) before deleting data
        UIManager.Instance.ShowMessage("Your game data was deleted. Starting over from scratch...");
        SaveSystem.HardResetSavedData();
        LoadData();
    }
}
