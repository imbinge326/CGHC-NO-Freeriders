using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    public static FinalLevelManager Instance { get; private set; } // Singleton

    [SerializeField]
    private GameObject chaseMobPrefab;

    [SerializeField]
    private GameObject chaseMobSpawnPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
        else
        {
            Instance = this; // Set singleton instance
        }
    }

    public void SpawnChaseMob()
    {
        if (chaseMobPrefab == null || chaseMobSpawnPoint == null)
        {
            Debug.LogError("Chase mob prefab or spawn point is not assigned!");
            return;
        }

        Instantiate(chaseMobPrefab, chaseMobSpawnPoint.transform.position, Quaternion.identity);
    }
}
