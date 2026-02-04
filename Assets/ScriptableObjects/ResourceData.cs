using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Clicker/Resource")]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public string description;
    public float chromaPerSecond;
    public int baseCost;
    public float costIncreaseRatio;
}
