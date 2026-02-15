using System;

[Serializable]
public class SaveData
{
    public double currentChroma;
    public int[] resourcesAmounts;
    public int pureChroma = 0;
    public long lastSaveTimestamp = 0;
    
    public SaveData(double currentChroma, int[] resourcesAmounts, int pureChroma)
    {
        UpdateSaveData(currentChroma, resourcesAmounts, pureChroma);
    }

    public SaveData UpdateSaveData(double currentChroma, int[] resourcesAmounts, int pureChroma)
    {
        this.currentChroma = currentChroma;
        this.resourcesAmounts = resourcesAmounts;
        this.pureChroma = pureChroma;
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

    public int GetPureChroma()
    {
        return pureChroma;
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
