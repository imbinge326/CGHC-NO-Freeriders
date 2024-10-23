using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Items> itemsList;
    public Transform itemContent;
    public GameObject inventoryItem;
    public GameObject inventoryUI; // 引用 Inventory UI
    public GameObject eventSystem; // 引用 EventSystem

    private void Awake()
    {
        // 确保 InventoryManager 是唯一的实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 确保 InventoryManager 和 InventoryUI 不被销毁
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(inventoryUI);
        DontDestroyOnLoad(eventSystem);

        // 确保只存在一个 EventSystem 和 InventoryUI
        HandleEventSystem();
        HandleInventoryUI();
    }

    private void OnEnable()
    {
        // 场景加载完成时触发
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 当场景加载完成时，确保 Inventory UI 是开启状态，并且处理 EventSystem
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 确保 inventoryUI 被激活
        if (inventoryUI != null && !inventoryUI.activeInHierarchy)
        {
            inventoryUI.SetActive(true); // 激活 Inventory UI
            ActivateAllChildren(inventoryUI); // 确保所有子对象也被激活
        }

        // 处理 EventSystem 和 InventoryUI，避免重复
        HandleEventSystem();
        HandleInventoryUI();
    }

    // 确保只存在一个 EventSystem
    private void HandleEventSystem()
    {
        // 查找当前场景中的所有 EventSystem
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

        // 如果存在多个 EventSystem，则销毁重复的
        if (eventSystems.Length > 1)
        {
            foreach (var es in eventSystems)
            {
                if (es.gameObject != eventSystem)
                {
                    Destroy(es.gameObject);
                }
            }
        }
    }

    // 确保只存在一个 InventoryUI
    private void HandleInventoryUI()
    {
        // 查找当前场景中的所有 InventoryUI
        InventoryManager[] inventoryManagers = FindObjectsOfType<InventoryManager>();

        // 如果存在多个 InventoryUI，则销毁重复的
        if (inventoryManagers.Length > 1)
        {
            foreach (var im in inventoryManagers)
            {
                if (im != this)
                {
                    Destroy(im.inventoryUI);
                }
            }
        }
    }

    // 递归激活所有子对象
    private void ActivateAllChildren(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
            ActivateAllChildren(child.gameObject); // 递归处理所有子对象
        }
    }

    // 添加物品到库存列表
    public void AddItems(Items items)
    {
        itemsList.Add(items);
        ListItems(); // 每次添加物品后更新UI
    }

    // 从库存列表中移除物品
    public void RemoveItems(Items items)
    {
        itemsList.Remove(items);
        ListItems(); // 每次移除物品后更新UI
    }

    // 列出当前库存中的物品
    public void ListItems()
    {
        // 清空当前 UI 列表
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        // 重新生成 UI 列表
        foreach (var items in itemsList)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("Item Name").GetComponent<TMP_Text>();
            itemName.text = items.itemName;
        }
    }
}
