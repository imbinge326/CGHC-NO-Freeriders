using UnityEngine;

public class RoleSwitcher : MonoBehaviour
{
    public GameObject role1Prefab;
    public GameObject role2Prefab;
    public GameObject role3Prefab;

    private GameObject currentRole;

    void Start()
    {
        // 游戏开始时，将当前角色设置为Role1
        currentRole = Instantiate(role1Prefab, transform.position, transform.rotation);
        currentRole.tag = "Player"; // 设置标签为Player，方便后续的控制和识别
    }

    void Update()
    {
        // 按1键切换到Role1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchRole(role1Prefab);
            Debug.Log("Role 1");
        }

        // 按2键切换到Role2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchRole(role2Prefab);
            Debug.Log("Role 2");
        }

        // 按3键切换到Role3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchRole(role3Prefab);
            Debug.Log("Role 3");
        }
    }

    void SwitchRole(GameObject newRolePrefab)
    {
        // 记录当前位置和旋转信息
        Vector3 position = currentRole.transform.position;
        Quaternion rotation = currentRole.transform.rotation;

        // 销毁当前角色
        if (currentRole != null)
        {
            Destroy(currentRole);
        }

        // 实例化新角色，并设置为当前角色
        currentRole = Instantiate(newRolePrefab, position, rotation);
        currentRole.tag = "Player"; // 确保新角色也有"Player"标签
    }
}
