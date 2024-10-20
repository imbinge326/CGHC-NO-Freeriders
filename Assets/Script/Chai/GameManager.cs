using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 记录哪些道具已经被捡取（通过 itemID）
    private HashSet<int> collectedItemIDs = new HashSet<int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 确保在切换场景时不会被销毁
    }

    // 检查某个 itemID 的道具是否已经被捡取
    public bool IsItemCollected(int itemID)
    {
        return collectedItemIDs.Contains(itemID);
    }

    // 标记道具已被捡取
    public void CollectItem(int itemID)
    {
        if (!collectedItemIDs.Contains(itemID))
        {
            collectedItemIDs.Add(itemID);
        }
    }
}
