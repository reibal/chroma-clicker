using UnityEngine;

public class ResourceRuntimeData
{
    public ResourceListItem resourceGameObject;
    public ResourceData resourceData;
    public int amount;

    public void Initialize(ResourceData resourceData)
    {
        this.resourceData = resourceData;
        // TODO: Load amount from save data
        amount = 0;
    }

    public void AssignGameObject(ResourceListItem gameObject)
    {
        resourceGameObject = gameObject;
    }

    public float GetUpgradeCost()
    {
        return resourceData.baseCost * Mathf.Pow(1 + resourceData.costIncreaseRatio, amount);
    }

    public float GetChromaPerSecond()
    {
        return resourceData.chromaPerSecond * amount;
    }

    public void IncreaseAmountBy(int increment)
    {
        amount += increment;
        resourceGameObject.UpdateAmountText();
        resourceGameObject.UpdatePurchaseButtonText();
    }
}
