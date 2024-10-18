using UnityEngine;

public class RoleSwitcher : MonoBehaviour
{
    public GameObject role1Prefab; // Knight
    public GameObject role2Prefab; // Wizard
    public GameObject role3Prefab; // Assassin

    private GameObject currentRole;
    private string currentRoleType; // 用于记录当前的角色类型


    void Start()
    {
        // 游戏开始时，将当前角色设置为Role1 (Knight)
        SwitchRole(role1Prefab, "Knight");
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
            SwitchRole(role2Prefab, "Wizard");
            Debug.Log("Switched to Wizard");
        }

        // 按3键切换到Assassin
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchRole(role3Prefab, "Assassin");
            Debug.Log("Switched to Assassin");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 保留对象在场景切换时不会被销毁
  
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

        // 可以在这里根据角色类型应用不同的逻辑
        HandleRoleSpecificLogic();
    }

    // 根据不同的角色类型设置逻辑
    void HandleRoleSpecificLogic()
    {
        switch (currentRoleType)
        {
            case "Knight":
                // 设定Knight的技能或其他功能
                Debug.Log("Knight can break the wall");
                break;

            case "Wizard":
                // 设定Wizard的技能或其他功能
                Debug.Log("Wizard can teleport.");
                break;

            case "Assassin":
                // 设定Assassin的技能或其他功能
                Debug.Log("Assassin can hide");
                break;
        }
    }
}
