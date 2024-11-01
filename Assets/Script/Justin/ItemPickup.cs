using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Items items;

    private void Start()
    {
        // 在场景加载时，检查该道具是否已经被捡取
        if (GameManager.Instance.IsItemCollected(items.itemID))
        {
            // 如果道具已经被捡取，直接销毁道具对象
            Destroy(gameObject);
        }
    }

    public void Pickup()
    {
        GameManager.Instance.CollectItem(items.itemID);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Pickup();
        //put trigger colliders on pickup
    }
}
