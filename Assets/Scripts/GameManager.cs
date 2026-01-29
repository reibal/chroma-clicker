using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private float currentChroma = 0f;
    private float chromaIncreasePerClick = 1f;

    // TODO: Remove, this is just for testing
    private float chromaPerSecond = 5f;
    private float loopDelay = 0.2f;

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
        StartCoroutine(Loop());
        UIManager.Instance.SetCurrentChroma(currentChroma);
        UIManager.Instance.SetChromaPerSecond(chromaPerSecond);
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
            currentChroma += chromaPerSecond * loopDelay;
            UIManager.Instance.SetCurrentChroma(currentChroma);
            yield return new WaitForSeconds(loopDelay);
        }
    }
}
