using System;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance { get; private set; }

    [Header("GameObjects")]
    [SerializeField] private GameObject resourcesContainer;
    [SerializeField] private ResourceListItem resourcePrefab;

    [Header("Resources")]
    [SerializeField] private ResourceData[] resourceDataList;
    private ResourceRuntimeData[] resources;

    // Public getters
    public ResourceRuntimeData[] Resources => resources;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeResources();
    }

    private void InitializeResources()
    {
        resources = new ResourceRuntimeData[resourceDataList.Length];
        bool renderNext = true;
        int[] resourcesAmounts = SaveSystem.Load().GetResourcesAmounts();
        for (int i = 0; i < resourceDataList.Length; i++)
        {
            resourceDataList[i].resourceIndex = i;
            resources[i] = new(resourceDataList[i]);
            if (i < resourcesAmounts.Length)
            {
                resources[i].SetAmount(resourcesAmounts[i]);
            }
            if (renderNext)
            {
                InstantiateResource(i);
            }
            renderNext = resources[i].Amount > 0;
        }
    }

    private void InstantiateResource(int resourceIndex)
    {
        if (resourceIndex >= resourceDataList.Length)
        {
            return;
        }
        ResourceListItem item = Instantiate(resourcePrefab, resourcesContainer.transform);
        resources[resourceIndex].AssignGameObject(item);
        item.Initialize(resources[resourceIndex]);
    }

    public void PurchaseResource(int resourceIndex)
    {
        ResourceRuntimeData resource = resources[resourceIndex];
        bool canAfford = GameManager.Instance.SpendChroma(resource.UpgradeCost);
        if (!canAfford)
        {
            Debug.Log($"Not enough Chroma to purchase {resource.ResourceName}. Required: {resource.UpgradeCost}, Current: {GameManager.Instance.CurrentChroma}");
            return;
        }
        Debug.Log($"Purchasing resource: {resource.ResourceName} for {resource.UpgradeCost} Chroma.");
        resource.IncreaseAmountBy(1);
        GameManager.Instance.RecalculateTotalChromaPerSecond();
        // If the player purchases this resource for the first time, and it's not the last tier, instantiate the next tier
        if (resources[resourceIndex].Amount == 1 && resourceIndex + 1 < resourceDataList.Length)
        {
            InstantiateResource(resourceIndex + 1);
        }
    }

    public float CalculateTotalChromaPerSecond()
    {
        float total = 0f;
        foreach (ResourceRuntimeData resource in resources)
        {
            total += resource.GeneratedChromaPerSecond;
        }
        return total;
    }

    public ResourceData[] GetResourceDataList()
    {
        return resourceDataList;
    }
}
