using UnityEngine;

public static class SaveSystem
{
    private static readonly string savePath = System.IO.Path.Combine(Application.persistentDataPath, "save.json");
    private static SaveData currentSaveData = null;

    public static void Save(float currentChroma, ResourceRuntimeData[] runtimeResources)
    {
        if (currentSaveData == null)
        {
            // This is a Safety Check: since Load() will always create a default save,
            //  if save data is null and we save, we might be saving data before loading
            //  (could overwrite the actual save)
            return;
        }

        int[] resources = new int[runtimeResources.Length];
        for (int i = 0; i < runtimeResources.Length; i++)
        {
            int resourceAmount = runtimeResources[i].Amount;
            resources[i] = resourceAmount;
        }

        UpdateSaveDataVar(currentChroma, resources);
        SaveToFile();
    }

    private static void SaveToFile()
    {
        string json = JsonUtility.ToJson(currentSaveData, true);
        System.IO.File.WriteAllText(savePath, json);
        Debug.Log("Saved at: " + savePath);
    }

    public static SaveData Load()
    {
        if (currentSaveData != null)
        {
            // This happens when data was already loaded from somewhere else
            return currentSaveData;
        }

        if (System.IO.File.Exists(savePath))
        {
            // Load saved data from file into the currentSaveData variable
            LoadFromFile();
        }
        else
        {
            // Create new save data if it does not exist yet
            CreateNewSaveData();
            Debug.Log("New save data created");
        }
        return currentSaveData;
    }

    private static SaveData LoadFromFile()
    {
        string json = System.IO.File.ReadAllText(savePath);
        currentSaveData = JsonUtility.FromJson<SaveData>(json);
        return currentSaveData;
    }


    public static SaveData CreateNewSaveData()
    {
        // Default values (if file does not exist yet)
        int currentChroma = 0; // int currentChroma = chromaFromFile || 0;
        int[] resources = GetDefaultResourceAmountsForSaveData(); // int[] resources = resourcesFromFile || GetDefaultResourceAmounts();
        return UpdateSaveDataVar(currentChroma, resources);
    }

    public static SaveData HardResetSavedData()
    {
        CreateNewSaveData();
        SaveToFile();
        return currentSaveData;
    }

    private static SaveData UpdateSaveDataVar(float currentChroma, int[] resources)
    {
        if (currentSaveData == null)
        {
            currentSaveData = new SaveData(currentChroma, resources);
        }
        else
        {
            currentSaveData.UpdateSaveData(currentChroma, resources);
        }
        return currentSaveData;
    }

    private static int[] GetDefaultResourceAmountsForSaveData()
    {
        ResourceData[] resourcesDataList = ResourcesManager.Instance.GetResourceDataList();
        int[] defaultAmounts = new int[resourcesDataList.Length];
        for (int i = 0; i < resourcesDataList.Length; i++)
        {
            defaultAmounts[i] = 0;
        }
        return defaultAmounts;
    }

}
