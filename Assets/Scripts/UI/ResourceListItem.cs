using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceListItem : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private TextMeshProUGUI chromaGeneratedTMP;
    [SerializeField] private TextMeshProUGUI purchaseButtonTMP;
    [SerializeField] private TextMeshProUGUI amountTMP;

    private ResourceRuntimeData resource;

    public void Initialize(ResourceRuntimeData resource)
    {
        this.resource = resource;
        iconImage.sprite = resource.ResourceIcon;
        titleTMP.text = resource.ResourceName;
        descriptionTMP.text = resource.ResourceDescription;
        UpdateChromaGeneratedText();
        UpdatePurchaseButtonText();
        UpdateAmountText();
    }

    public void OnPurchaseButtonClicked()
    {
        ResourcesManager.Instance.PurchaseResource(resource.ResourceIndex);
    }

    public void UpdateChromaGeneratedText()
    {
        string chromaPerSecondEachResourceString = Utils.FormatBigNumber(resource.ChromaPerSecondEachResource);
        string totalChromaPerSecondString = Utils.FormatBigNumber(resource.TotalChromaPerSecond);
        chromaGeneratedTMP.text = $"+{totalChromaPerSecondString} <sprite index=0>/s  (+{chromaPerSecondEachResourceString} <sprite index=0>/s each)";
    }

    public void UpdatePurchaseButtonText()
    {
        string upgradeCostString = Utils.FormatBigNumber(resource.UpgradeCost);
        purchaseButtonTMP.text = $"Buy for {upgradeCostString} Chroma";
    }

    public void UpdateAmountText()
    {
        amountTMP.text = resource.Amount.ToString();
    }

}
