using System.Collections.Generic;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    public static FinalLevelManager Instance { get; private set; } // Singleton

    [SerializeField]
    private GameObject chaseMobPrefab;

    [SerializeField]
    private GameObject chaseMobSpawnPoint;

    [SerializeField]
    private Transform[] targetPoints; // Array of target points

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

        GameObject chaseMob = Instantiate(chaseMobPrefab, chaseMobSpawnPoint.transform.position, Quaternion.identity);

        // Get the PointMover component and set the target points
        FollowPath pointMover = chaseMob.GetComponent<FollowPath>();
        if (pointMover != null)
        {
            List<Transform> targetPointsList = new List<Transform>(targetPoints); // Convert array to list
            pointMover.SetTargetPoints(targetPointsList); // Assign the target points
        }
    }
}
