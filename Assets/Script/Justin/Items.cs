using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public float itemValue;
    public enum ItemType {key, potion, dynamite};
    public ItemType itemType;
}
