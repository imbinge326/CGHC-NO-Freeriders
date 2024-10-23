using UnityEngine;
using UnityEngine.SceneManagement; // 需要引入SceneManagement来加载场景

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;  // 想跳转的场景名字
    [SerializeField] public bool useReturnPoint = false; // 决定是否使用返回点

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if (collision.CompareTag("Player"))
        {
            // 获取玩家控制器
            PlayerController player = collision.GetComponent<PlayerController>();

            // 设置是否使用 ReturnPoint
            if (player != null)
            {
                player.useReturnPoint = useReturnPoint;
            }

            // 跳转到下一个场景
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
