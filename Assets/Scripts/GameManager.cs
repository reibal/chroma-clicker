using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float currentChroma = 0f;
    private float chromaIncreasePerClick = 1f;

    private float chromaPerSecond = 0f;

    private readonly float loopDelaySeconds = 0.05f; // 0.05f delay = 20 recalculations per second
    private readonly int autosaveDelaySeconds = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Load saved data
        SaveData saveData = SaveSystem.Load();
        // Total chroma
        currentChroma = saveData.GetCurrentChroma();
        UIManager.Instance.SetCurrentChroma(saveData.currentChroma);
        // Chroma per second
        RecalculateTotalChromaPerSecond();
        // Add extra afk-farmed chroma
        AddAfkFarmedChroma(saveData.GetSecondsFromLastSession());
        // Coroutine loops
        StartCoroutine(ChromaPerSecondLoop());
        StartCoroutine(AutoSaveLoop());
    }

    void OnApplicationQuit()
    {
        SaveData();
    }


    public void OnChromaClicked()
    {
        currentChroma += chromaIncreasePerClick;
        UIManager.Instance.SetCurrentChroma(currentChroma);
    }

    IEnumerator ChromaPerSecondLoop()
    {
        while (true)
        {
            currentChroma += chromaPerSecond * loopDelaySeconds;
            UIManager.Instance.SetCurrentChroma(currentChroma);
            yield return new WaitForSeconds(loopDelaySeconds);
        }
    }

    IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            SaveData();
            yield return new WaitForSeconds(autosaveDelaySeconds);
        }
    }

    private void SaveData()
    {
        SaveSystem.Save(currentChroma, ResourcesManager.Instance.GetResourceRuntimeDataList());
    }

    public float GetCurrentChroma()
    {
        return currentChroma;
    }

    public bool SpendChroma(float amount)
    {
        if (currentChroma >= amount)
        {
            currentChroma -= amount;
            UIManager.Instance.SetCurrentChroma(currentChroma);
            return true;
        }
        return false;
    }

    private void AddAfkFarmedChroma(long elapsedSeconds)
    {
        float obtainedChroma = chromaPerSecond * elapsedSeconds * 0.4f;
        UIManager.Instance.ShowMessage("You got " + obtainedChroma + " chroma while you were AFK.");
        currentChroma += obtainedChroma;
    }

    public void RecalculateTotalChromaPerSecond()
    {
        chromaPerSecond = ResourcesManager.Instance.CalculateTotalChromaPerSecond();
        UIManager.Instance.SetChromaPerSecond(chromaPerSecond);
        chromaIncreasePerClick = 1f + (chromaPerSecond * 0.05f); // Each click is = 1 + (5% of cps)
    }
}
