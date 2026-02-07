using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI currentChromaText;
    [SerializeField]
    private TextMeshProUGUI chromaPerSecondText;

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

    public void OnChromaClicked()
    {
        GameManager.Instance.OnChromaClicked();
    }

    public void SetCurrentChroma(float amount)
    {
        currentChromaText.text = Mathf.FloorToInt(amount).ToString() + " Chroma";
    }

    public void SetChromaPerSecond(float amount)
    {
        // Show up to 2 decimals 
        chromaPerSecondText.text = $"{amount:F2} cps";
    }
}
