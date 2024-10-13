using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Items items;

    public void Pickup()
    {
        InventoryManager.Instance.AddItems(items);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Pickup();
        //put trigger colliders on pickup
    }
}
