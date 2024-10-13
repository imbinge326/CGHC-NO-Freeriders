using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public int itemID;
    public string itemName;
    public enum ItemType {key, potion};
}
