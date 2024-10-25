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

    private GameObject player;
    private Vector3 playerPosition; // Variable to hold the player's position

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

        // Find player reference in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Store the player's initial position
            playerPosition = player.transform.position;
        }

        StartCoroutine(UnfreezePlayer());
    }

    private IEnumerator UnfreezePlayer()
    {
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null; // Wait for the next frame
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None; // Unfreeze the player
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
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

        FollowPath pointMover = chaseMob.GetComponent<FollowPath>();
        if (pointMover != null)
        {
            List<Transform> targetPointsList = new List<Transform>(targetPoints);
            pointMover.SetTargetPoints(targetPointsList);
        }
    }

    public void TriggerBossFight()
    {
        // Save the player's current position before loading the cutscene
        if (player != null)
        {
            playerPosition = player.transform.position; // Save player position
        }

        StartCoroutine(FadeAndLoadScene("BossCutscene"));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        float fadeDuration = 1f; // Duration of the fade
        float elapsed = 0f;

        fadeImage.color = new Color(0, 0, 0, 0);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
