using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public enum ItemType {key, potion};
    public ItemType itemType;
}
