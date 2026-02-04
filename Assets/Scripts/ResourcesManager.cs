using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    
    [SerializeField] private GameObject resourcesContainer;
    [SerializeField] private ResourceListItem resourcePrefab;
    [SerializeField] private ResourceData[] resources;

    void Start()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            ResourceListItem item = Instantiate(resourcePrefab, resourcesContainer.transform);
            item.Initialize(resources[i]);
        }
    }
}
