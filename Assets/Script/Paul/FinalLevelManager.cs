using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Tilemaps;

public class FinalLevelManager : MonoBehaviour
{
    public static FinalLevelManager Instance { get; private set; } // Singleton

    [Header("Chase Mob Settings")]
    [SerializeField]
    private GameObject chaseMobPrefab;
    [SerializeField]
    private GameObject chaseMobSpawnPoint;
    [SerializeField]
    private Transform[] targetPoints; // Array of target points

    [Header("UI Settings")]
    [SerializeField]
    private Image fadeImage; // Assign your FadeImage here in the Inspector
    [SerializeField]
    private GameObject dynamitePickupTextObject;
    [SerializeField]
    private GameObject getDynamiteTextObject;

    [Header("Others")]
    [SerializeField]
    private GameObject triggerBossLevelComponent;
    [SerializeField]
    private GameObject floorBreakTilemap;

    private GameObject player;

    public bool hasDynamite = false;
    public static Vector3 playerPosition; // Variable to hold the player's position

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
        else
        {
            Instance = this; // Set singleton instance
            StartCoroutine(DelayedFindPlayer()); // Start coroutine to delay FindPlayer
        }
    }

    private void Start()
    {
        dynamitePickupTextObject.SetActive(false);
        getDynamiteTextObject.SetActive(false);
    }

    private IEnumerator DelayedFindPlayer()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        FindPlayer();
    }

    private void FindPlayer()
    {
        // Find player reference in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePosition; // Unfreeze the player
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
        }
    }

    // When player first goes into dynamite room without keys
    public void ActivateDynamiteQuest()
    {
        getDynamiteTextObject.SetActive(true);
        StartCoroutine(DeactivateObjectAfterSeconds(getDynamiteTextObject, 4f));
    }

    // When player picks up dynamite
    public void OnDynamitePickup()
    {
        dynamitePickupTextObject.SetActive(true);
        StartCoroutine(DeactivateObjectAfterSeconds(dynamitePickupTextObject, 3f));
        hasDynamite = true;
    }

    // When player goes back into dynamite room with dynamite
    public void UseDynamite()
    {
        floorBreakTilemap.SetActive(false);
    }

    private IEnumerator ActivateObjectAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(true);
    }

    private IEnumerator DeactivateObjectAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
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
        if (player != null)
        {
            playerPosition = player.transform.position;
        }

        StartCoroutine(FadeAndLoadScene("BossCutscene"));
        PlayerController.instance.playerPosition = playerPosition;
        PlayerController.instance.cutsceneLoad = true;
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        float fadeDuration = 1f;
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
