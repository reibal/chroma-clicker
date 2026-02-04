using TMPro;
using UnityEngine;

public class ResourceListItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;


    public void Initialize(ResourceData resourceData)
    {
        titleTMP.text = resourceData.resourceName;
        descriptionTMP.text = resourceData.description;        
    }
}
