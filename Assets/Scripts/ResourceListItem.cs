using TMPro;
using UnityEngine;

public class ResourceListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private TextMeshProUGUI purchaseButtonTMP;
    [SerializeField] private TextMeshProUGUI amountTMP;

    private ResourceRuntimeData resource;

    public void Initialize(ResourceRuntimeData resource)
    {
        this.resource = resource;
        titleTMP.text = resource.resourceData.resourceName;
        descriptionTMP.text = resource.resourceData.description;
        UpdatePurchaseButtonText();
        UpdateAmountText();
    }

    public void OnPurchaseButtonClicked()
    {
        ResourcesManager.Instance.PurchaseResource(resource.resourceData.resourceIndex);
    }

    public void UpdatePurchaseButtonText()
    {
        purchaseButtonTMP.text = $"Buy for {resource.GetUpgradeCost()} Chroma";
    }
    
    public void UpdateAmountText()
    {
        amountTMP.text = resource.amount.ToString();
    }

}
