using UnityEngine;

public class RoleSwitcher : MonoBehaviour
{
    public GameObject role1Prefab; // Knight
    public GameObject role2Prefab; // Wizard
    public GameObject role3Prefab; // Assassin
    public bool canSwitch = true;

    private GameObject currentRole;
    private string currentRoleType; // 用于记录当前的角色类型

    void Awake()
    {
        // 游戏开始时，将当前角色设置为Role1 (Knight)
        currentRole = GameObject.FindGameObjectWithTag("Player");
        if (currentRole == null)
        {
            SwitchRole(role1Prefab, "Knight");
            Debug.LogWarning("spawned");
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!canSwitch)
            return;

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

    void SwitchRole(GameObject newRolePrefab, string roleType)
    {
        // 如果没有新的角色预制件，不进行任何操作
        if (newRolePrefab == null)
        {
            Debug.LogError("New role prefab is null!");
            return;
        }

        // 记录当前位置和旋转信息，避免传送回 RoleSwitcher 位置
        Vector3 position = currentRole != null ? currentRole.transform.position : transform.position;
        Quaternion rotation = currentRole != null ? currentRole.transform.rotation : transform.rotation;

        // 销毁当前角色
        if (currentRole != null)
        {
            Destroy(currentRole);
        }

        // 确保预制件成功生成
        currentRole = Instantiate(newRolePrefab, position, rotation);
        if (currentRole == null)
        {
            Debug.LogError("Failed to instantiate role prefab!");
            return;
        }

        currentRole.tag = "Player"; // 确保新角色也有"Player"标签

        // 更新当前角色类型
        currentRoleType = roleType;
        DontDestroyOnLoad(currentRole);
    }
}
