using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI currentChromaText;
    [SerializeField]
    private TextMeshProUGUI chromaPerSecondText;

    [Header("Pop Up Windows")]
    [SerializeField] private InfoMessageWindow infoMessageWindow;
    [SerializeField] private ConfirmMessageWindow confirmMessageWindow;

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

    public void SetCurrentChromaUI(double amount)
    {
        string currentChromaFormatted = Utils.FormatBigNumber(amount, false);
        currentChromaText.text = $"{currentChromaFormatted} Chroma";
    }

    public void SetChromaPerSecondUI(double chromaAmount, float prestigeBonus = 0f)
    {
        // Fix to also show 2 decimals when below 1K
        string amountString = chromaAmount > 1000 ? Utils.FormatBigNumber(chromaAmount) : $"{chromaAmount:F2}";
        string prestigeBonusString = prestigeBonus > 0f ? $"(+{prestigeBonus:0.##}%)": "";
        chromaPerSecondText.text = $"{amountString} cps {prestigeBonusString}";
    }

    public void ShowInfoMessage(string message)
    {
        infoMessageWindow.ShowMessage(message);
    }

    public void ShowConfirmMessage(string message, Action callbackOnConfirm)
    {
        confirmMessageWindow.SetCallbackOnConfirm(callbackOnConfirm);
        confirmMessageWindow.ShowMessage(message);
    }
}
