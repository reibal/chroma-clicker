using System;
using UnityEngine;

public class ResourceRuntimeData
{
    public ResourceListItem resourceGameObject;
    private ResourceData resourceData;
    private int amount;

    // Public getters (simple)
    public int ResourceIndex => resourceData.resourceIndex;
    public int Amount => amount;
    public string ResourceName => resourceData.resourceName;
    public string ResourceDescription => resourceData.description;
    public Sprite ResourceIcon => resourceData.icon;

    // Public getters (calculated)
    public int UpgradeCost => Mathf.CeilToInt(resourceData.baseCost * Mathf.Pow(1 + resourceData.costIncreaseRatio, amount));
    public float GeneratedChromaPerSecond => resourceData.chromaPerSecond * amount;

    public ResourceRuntimeData(ResourceData resourceData)
    {
        this.resourceData = resourceData;
        amount = 0;
    }

    public void AssignGameObject(ResourceListItem gameObject)
    {
        resourceGameObject = gameObject;
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
        if (resourceGameObject != null)
        {
            resourceGameObject.UpdateAmountText();
            resourceGameObject.UpdatePurchaseButtonText();
        }
    }

    public void IncreaseAmountBy(int increment)
    {
        amount += increment;
        resourceGameObject.UpdateAmountText();
        resourceGameObject.UpdatePurchaseButtonText();
    }
}
