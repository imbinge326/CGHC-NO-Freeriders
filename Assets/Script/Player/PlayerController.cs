using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    // 用于区分玩家是否是从上一场景或下一场景传送过来的
    public bool useReturnPoint = false;

    private void Awake()
    {
        // 确保 PlayerController 是唯一的实例，并不被销毁
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
