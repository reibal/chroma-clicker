using System;

[Serializable]
public class SaveData
{
    public double currentChroma;
    public int[] resourcesAmounts;
    public long lastSaveTimestamp = 0;
    
    public SaveData(double currentChroma, int[] resourcesAmounts)
    {
        UpdateSaveData(currentChroma, resourcesAmounts);
    }

    public SaveData UpdateSaveData(double currentChroma, int[] resourcesAmounts)
    {
        this.currentChroma = currentChroma;
        this.resourcesAmounts = resourcesAmounts;
        lastSaveTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public double GetCurrentChroma()
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
