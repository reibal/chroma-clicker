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
        SaveData saveData = SaveSystem.Load();
        currentChroma = saveData.GetCurrentChroma();
    }

    void Start()
    {
        SaveData saveData = SaveSystem.Load();
        UIManager.Instance.SetCurrentChroma(saveData.currentChroma);
        RecalculateTotalChromaPerSecond();
        StartCoroutine(Loop());
        StartCoroutine(AutoSave());
    }

    void OnApplicationQuit()
    {
        SaveSystem.Save(currentChroma, ResourcesManager.Instance.GetResourceRuntimeDataList());
    }


    public void OnChromaClicked()
    {
        currentChroma += chromaIncreasePerClick;
        UIManager.Instance.SetCurrentChroma(currentChroma);
    }

    IEnumerator Loop()
    {
        while (true)
        {
            currentChroma += chromaPerSecond * loopDelaySeconds;
            UIManager.Instance.SetCurrentChroma(currentChroma);
            yield return new WaitForSeconds(loopDelaySeconds);
        }
    }

    IEnumerator AutoSave()
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

    public void RecalculateTotalChromaPerSecond()
    {
        chromaPerSecond = ResourcesManager.Instance.CalculateTotalChromaPerSecond();
        UIManager.Instance.SetChromaPerSecond(chromaPerSecond);
        chromaIncreasePerClick = 1f + (chromaPerSecond * 0.05f); // Each click is = 1 + (5% of cps)
    }
}
