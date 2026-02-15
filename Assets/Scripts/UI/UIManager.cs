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

    [Header("Effects")]
    [SerializeField] private ParticleSystem clickParticleSystemPrefab;

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

    public void ShowDisplayInfoOnChromaClicked() {
        // Show particle system on click
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane + 1f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Instantiate(clickParticleSystemPrefab, worldPosition, Quaternion.identity);
    }
}
