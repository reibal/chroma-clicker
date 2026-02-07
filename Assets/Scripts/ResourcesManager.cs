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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        resources = new ResourceRuntimeData[resourceDataList.Length];

        bool renderNext = true;
        for (int i = 0; i < resourceDataList.Length; i++)
        {
            resourceDataList[i].resourceIndex = i;
            ResourceRuntimeData resource = new();
            resource.Initialize(resourceDataList[i]);
            resources[i] = resource;
            if (renderNext)
            {
                InstantiateResource(i);
            }
            if (resource.amount <= 0)
            {
                renderNext = false;
            }
        }
    }

    private void InstantiateResource(int resourceIndex)
    {
        ResourceListItem item = Instantiate(resourcePrefab, resourcesContainer.transform);
        resources[resourceIndex].AssignGameObject(item);
        item.Initialize(resources[resourceIndex]);
    }

    public void PurchaseResource(int resourceIndex)
    {
        ResourceRuntimeData resource = resources[resourceIndex];
        float cost = resource.GetUpgradeCost();
        bool canAfford = GameManager.Instance.SpendChroma(cost);
        if (!canAfford)
        {
            Debug.Log($"Not enough Chroma to purchase {resource.resourceData.resourceName}. Required: {cost}, Current: {GameManager.Instance.GetCurrentChroma()}");
            return;
        }
        Debug.Log($"Purchasing resource: {resource.resourceData.resourceName} for {cost} Chroma.");
        resource.IncreaseAmountBy(1);
        GameManager.Instance.RecalculateTotalChromaPerSecond();
        // If this is the first purchased resource of this tier, and it's not the last tier, instantiate the next tier
        if (resources[resourceIndex].amount == 1 && resourceIndex + 1 < resourceDataList.Length)
        {
            InstantiateResource(resourceIndex + 1);
        }
    }

    public float CalculateTotalChromaPerSecond()
    {
        float total = 0f;
        foreach (ResourceRuntimeData resource in resources)
        {
            total += resource.GetChromaPerSecond();
        }
        return total;
    }

}
