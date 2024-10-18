using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Items> itemsList;
    public Transform itemContent;
    public GameObject inventoryItem;

    public void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddItems(Items items)
    {
        itemsList.Add(items);
    }

    public void RemoveItems(Items items)
    {
        itemsList.Remove(items);
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var items in itemsList)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("Item Name").GetComponent<TMP_Text>();

            itemName.text = items.itemName;
        }
    }
}
