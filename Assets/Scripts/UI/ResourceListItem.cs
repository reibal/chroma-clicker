using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceListItem : MonoBehaviour
{
    private ResourceRuntimeData resource;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private TextMeshProUGUI chromaGeneratedTMP;
    [SerializeField] private TextMeshProUGUI purchaseButtonTextTMP;
    [SerializeField] private TextMeshProUGUI amountTMP;

    private static Color ENABLED_BG_COLOR = new Color(1f, 1f, 1f, 1f);
    private static Color DISABLED_BG_COLOR = new Color(0.4f, 0.4f, 0.4f, 1f);

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
        string chromaPerSecondEachResourceString = Utils.FormatBigNumber(resource.ChromaPerSecondEachResource, true);
        string totalChromaPerSecondString = Utils.FormatBigNumber(resource.TotalChromaPerSecond, true);
        chromaGeneratedTMP.text = $"+{totalChromaPerSecondString} <sprite index=0>/s  (+{chromaPerSecondEachResourceString} <sprite index=0>/s each)";
    }

    public void UpdatePurchaseButtonText()
    {
        string upgradeCostString = Utils.FormatBigNumber(resource.NextUpgradeCost);
        purchaseButtonTextTMP.text = $"Buy for {upgradeCostString} Chroma";
    }

    public void UpdateAmountText()
    {
        amountTMP.text = resource.Amount.ToString();
    }

    public void DisablePurchaseButton()
    {
        purchaseButtonTextTMP.color = DISABLED_BG_COLOR;
    }

    public void EnablePurchaseButton()
    {
        purchaseButtonTextTMP.color = ENABLED_BG_COLOR;
    }
}
