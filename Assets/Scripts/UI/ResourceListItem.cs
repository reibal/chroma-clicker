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
        chromaGeneratedTMP.text = $"+{resource.ChromaPerSecondEachResource} <sprite index=0> each. TOTAL: +{resource.TotalChromaPerSecond} <sprite index=0>.";
    }

    public void UpdatePurchaseButtonText()
    {
        purchaseButtonTMP.text = $"Buy for {resource.UpgradeCost} Chroma";
    }
    
    public void UpdateAmountText()
    {
        amountTMP.text = resource.Amount.ToString();
    }

}
