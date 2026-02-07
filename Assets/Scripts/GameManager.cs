using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float currentChroma = 0f;
    private float chromaIncreasePerClick = 1f;

    private float chromaPerSecond = 0f;

    private float loopDelaySeconds = 0.125f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // TODO: Load from saved data if it exists
        UIManager.Instance.SetCurrentChroma(currentChroma);
        RecalculateTotalChromaPerSecond();
        StartCoroutine(Loop());
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
