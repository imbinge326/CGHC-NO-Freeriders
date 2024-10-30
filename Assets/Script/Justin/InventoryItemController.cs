using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private Items item;

    public void RemoveItemFromController()
    {
        InventoryManager.Instance.RemoveItems(item);
        Destroy(gameObject);
    }

    public void AddItemToController(Items newItem)
    {
        item = newItem;
    }

    public void UseItem(Items items)
    {
        switch (item.itemType)
        {
            case Items.ItemType.potion:
                HealthManager.Instance.Heal(item.itemValue);
                break;
            case Items.ItemType.key:
                break;
            case Items.ItemType.dynamite:
                break;
        }

        RemoveItemFromController();
    }
}
