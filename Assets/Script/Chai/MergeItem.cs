using UnityEngine;

public class MergeItem : MonoBehaviour
{
    [SerializeField]
    private GameObject item1; // 物品1
    [SerializeField]
    private GameObject item2; // 物品2
    [SerializeField]
    private GameObject potion; // 合成后生成的新物品

    private bool itemsCombined = false; // 确保物品只合成一次

    private void Start()
    {
        // 确保newItem一开始是隐藏的
        potion.SetActive(false);
    }

    private void Update()
    {
        // 检测两个物品是否碰撞，且尚未合成
        if (!itemsCombined && AreItemsColliding())
        {
            CombineItems();
            itemsCombined = true;
        }
    }

    // 检查是否碰撞
    private bool AreItemsColliding()
    {
        // 使用2D物理来检测物品碰撞
        if (item1 != null && item2 != null)
        {
            Collider2D item1Collider = item1.GetComponent<Collider2D>();
            Collider2D item2Collider = item2.GetComponent<Collider2D>();

            // 如果两个物体的Collider碰撞了
            return item1Collider.IsTouching(item2Collider);
        }
        return false;
    }

    // 合成物品的逻辑
    private void CombineItems()
    {
        // 隐藏原始的两个物品
        item1.SetActive(false);
        item2.SetActive(false);

        // 显示合成的新物品
        potion.SetActive(true);

        // 设置新物品的位置为物品1的位置（或者自定义位置）
        potion.transform.position = item1.transform.position;
    }
}
