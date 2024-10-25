using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure to include this for UI elements
using System.Collections; // Include this for the coroutine

public class FinalLevelManager : MonoBehaviour
{
    public static FinalLevelManager Instance { get; private set; } // Singleton

    [SerializeField]
    private GameObject chaseMobPrefab;

    [SerializeField]
    private GameObject chaseMobSpawnPoint;

    [SerializeField]
    private Transform[] targetPoints; // Array of target points

    [SerializeField]
    private Image fadeImage; // Assign your FadeImage here in the Inspector

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

    public void TriggerBossFight()
    {
        StartCoroutine(FadeAndLoadScene("BossCutscene"));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Fade to black
        float fadeDuration = 1f; // Duration of the fade
        float elapsed = 0f;

        // Set initial color to transparent
        fadeImage.color = new Color(0, 0, 0, 0);

        // Fade in
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
