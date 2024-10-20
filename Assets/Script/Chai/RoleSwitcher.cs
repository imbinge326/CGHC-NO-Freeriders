using UnityEngine;

public class RoleSwitcher : MonoBehaviour
{
    public GameObject role1Prefab; // Knight
    public GameObject role2Prefab; // Wizard
    public GameObject role3Prefab; // Assassin

    private GameObject currentRole;
    private string currentRoleType; // 用于记录当前的角色类型

    // 添加静态变量来保存单例
    private static RoleSwitcher instance;

    void Start()
    {
        // 游戏开始时，将当前角色设置为Role1 (Knight)
        if (currentRole == null)
        {
            SwitchRole(role1Prefab, "Knight");
        }
    }

    void Update()
    {
        // 按1键切换到Knight
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchRole(role1Prefab, "Knight");
            Debug.Log("Switched to Knight");
        }

        // 按2键切换到Wizard
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchRole(role2Prefab, "Mage");
            Debug.Log("Switched to Wizard");
        }

        // 按3键切换到Assassin
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchRole(role3Prefab, "Assassin");
            Debug.Log("Switched to Assassin");
        }
    }

    public void Awake()
    {
        // 检查是否已有实例，如果存在则销毁新对象
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // 防止重复生成
            return;
        }

        // 如果是唯一实例，保留并标记为不销毁
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void SwitchRole(GameObject newRolePrefab, string roleType)
    {
        // 记录当前位置和旋转信息
        Vector3 position = currentRole != null ? currentRole.transform.position : transform.position;
        Quaternion rotation = currentRole != null ? currentRole.transform.rotation : transform.rotation;

        // 销毁当前角色
        if (currentRole != null)
        {
            Destroy(currentRole);
        }

        // 实例化新角色，并设置为当前角色
        currentRole = Instantiate(newRolePrefab, position, rotation);
        currentRole.tag = "Player"; // 确保新角色也有"Player"标签

        // 更新当前角色类型
        currentRoleType = roleType;
        DontDestroyOnLoad (currentRole);  
    }
}
