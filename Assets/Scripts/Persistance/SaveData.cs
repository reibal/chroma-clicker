using System;

[Serializable]
public class SaveData
{
    public float currentChroma;
    public int[] resourcesAmounts;
    public long lastSaveTimestamp = 0;
    
    public SaveData(float currentChroma, int[] resourcesAmounts)
    {
        UpdateSaveData(currentChroma, resourcesAmounts);
    }

    public SaveData UpdateSaveData(float currentChroma, int[] resourcesAmounts)
    {
        this.currentChroma = currentChroma;
        this.resourcesAmounts = resourcesAmounts;
        lastSaveTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
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

    public long GetSecondsFromLastSession()
    {
        if(lastSaveTimestamp == 0)
        {
            return 0;
        }
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return now - lastSaveTimestamp;
    }
}
