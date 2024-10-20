using UnityEngine;
using UnityEngine.SceneManagement; // 需要引入SceneManagement来加载场景

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // 你想跳转的场景名字

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if (collision.CompareTag("Player"))
        {
            // 跳转到下一个场景
            SceneManager.LoadScene(nextSceneName);
        }
    }
}