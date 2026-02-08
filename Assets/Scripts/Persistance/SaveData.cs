[System.Serializable]
public class SaveData
{
    public float currentChroma;
    public int[] resourcesAmounts;
    
    public SaveData(float currentChroma, int[] resourcesAmounts)
    {
        UpdateSaveData(currentChroma, resourcesAmounts);
    }

    public SaveData UpdateSaveData(float currentChroma, int[] resourcesAmounts)
    {
        this.currentChroma = currentChroma;
        this.resourcesAmounts = resourcesAmounts;
        return this;
    }

    public float GetCurrentChroma()
    {
        return currentChroma;
    }

    public int[] GetResourcesAmounts()
    {
        return resourcesAmounts;
    }
}
