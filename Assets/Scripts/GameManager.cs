using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float currentChroma = 0f;
    private float chromaIncreasePerClick = 1f;

    // TODO: Remove this initialization, this is just for testing
    private float chromaPerSecond = 2f;

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
		// TODO: Calculate from upgrades
        UIManager.Instance.SetChromaPerSecond(chromaPerSecond);
		// TODO: Also set chroma per click, calculated from upgrades
		//chromaIncreasePerClick = ...;
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
}
