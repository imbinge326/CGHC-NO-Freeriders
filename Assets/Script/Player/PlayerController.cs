using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // 用于区分玩家是否是从上一场景或下一场景传送过来的
    public bool useReturnPoint = false;
    public bool cutsceneLoad = false;
    public Vector3 playerPosition;

    private void OnEnable()
    {
        // 订阅场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 取消订阅场景加载事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 当新场景加载时，根据是否使用返回点，选择生成点
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint;

        if (cutsceneLoad)
        {
            transform.position = playerPosition;
            return;
        }

        // 根据是否使用 ReturnPoint 选择生成点
        if (useReturnPoint)
        {
            spawnPoint = GameObject.Find("ReturnPoint");
        }
        else
        {
            spawnPoint = GameObject.Find("SpawnPoint");
        }

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("SpawnPoint or ReturnPoint not found in the scene.");
        }
    }
}