using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalLevelManager : MonoBehaviour
{
    public static FinalLevelManager Instance { get; private set; } // Singleton

    [Header("Chase Mob Settings")]
    [SerializeField]
    private GameObject chaseMobPrefab;
    [SerializeField]
    private GameObject chaseMobSpawnPoint;
    [SerializeField]
    private Transform[] targetPoints;

    [Header("UI Settings")]
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private GameObject dynamitePickupTextObject;
    [SerializeField]
    private GameObject getDynamiteTextObject;
    [SerializeField]
    private GameObject bossRoomOpenedTextObject;
    [SerializeField]
    private GameObject darkGodRuinsTextObject;

    [Header("Others")]
    [SerializeField]
    private GameObject triggerBossLevelComponent;
    [SerializeField]
    private GameObject floorBreakTilemap;
    [SerializeField]
    private GameObject bossRoomDoor;
    [SerializeField]
    private GameObject blockExit;

    private GameObject player;
    private GameObject roleSwitcher;
    private TextMeshProUGUI darkGodRuinsText;

    public bool hasDynamite = false;
    public static Vector3 playerPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            StartCoroutine(DelayedFindPlayer());
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        Cursor.visible = true;
    }

    private void Start()
    {
        darkGodRuinsTextObject.SetActive(false);
        blockExit.SetActive(false);
        triggerBossLevelComponent.SetActive(false);
        dynamitePickupTextObject.SetActive(false);
        getDynamiteTextObject.SetActive(false);
        bossRoomOpenedTextObject.SetActive(false);
    }

    private IEnumerator ShowDarkGodRuinsTextEffect()
    {
        // Timing settings - much slower for dramatic effect
        float screenFadeDuration = 3.0f;     // Slower fade from black
        float textDelay = 1.0f;              // Wait before showing text
        float textFadeInDuration = 2.0f;     // Slower text appearance
        float textHoldDuration = 4.0f;       // Hold text longer
        float textFadeOutDuration = 2.0f;    // Slower fade out

        // Start with black screen
        fadeImage.color = new Color(0, 0, 0, 1);

        // Gradually fade from black to clear
        yield return new WaitForSeconds(1.0f); // Initial pause in black
        for (float t = 0; t < screenFadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / screenFadeDuration;
            // Use easing function for smoother fade
            float alpha = 1 - Mathf.Pow(normalizedTime, 2);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);

        // Wait before showing text
        yield return new WaitForSeconds(textDelay);

        // Setup text initial state
        Color startColor = darkGodRuinsText.color;
        darkGodRuinsText.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        float initialScale = 1.8f;           // Start larger
        float targetScale = 2.0f;            // End even larger
        darkGodRuinsTextObject.transform.localScale = Vector3.one * initialScale;
        darkGodRuinsTextObject.SetActive(true);

        // Fade in text with subtle scale
        for (float t = 0; t < textFadeInDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / textFadeInDuration;
            // Use smooth step for more elegant easing
            float smoothValue = normalizedTime * normalizedTime * (3 - 2 * normalizedTime);
            darkGodRuinsText.color = new Color(startColor.r, startColor.g, startColor.b, smoothValue);
            darkGodRuinsTextObject.transform.localScale = Vector3.one * Mathf.Lerp(initialScale, targetScale, smoothValue);
            yield return null;
        }
        darkGodRuinsText.color = new Color(startColor.r, startColor.g, startColor.b, 1);
        darkGodRuinsTextObject.transform.localScale = Vector3.one * targetScale;

        // Hold the text
        yield return new WaitForSeconds(textHoldDuration);

        // Fade out text
        for (float t = 0; t < textFadeOutDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / textFadeOutDuration;
            // Use smooth step for fade out too
            float smoothValue = 1 - (normalizedTime * normalizedTime * (3 - 2 * normalizedTime));
            darkGodRuinsText.color = new Color(startColor.r, startColor.g, startColor.b, smoothValue);
            yield return null;
        }

        // Ensure text is fully transparent and disabled
        darkGodRuinsText.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        darkGodRuinsTextObject.SetActive(false);
    }

    private void Update()
    {
        // DEBUGGING
        if (Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene(7);
        }
    }

    private IEnumerator DelayedFindPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        FindPlayer();
        FindRoleSwitcher();
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
            Debug.LogError("Player has no PlayerController");

        if (playerController.cutsceneLoad)
        {
            blockExit.SetActive(true);
            FinalBossManager.Instance.StartBossFight();
            fadeImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            darkGodRuinsText = darkGodRuinsTextObject.GetComponent<TextMeshProUGUI>();
            StartCoroutine(ShowDarkGodRuinsTextEffect());
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
        }
    }

    void FindRoleSwitcher()
    {
        roleSwitcher = GameObject.Find("SwitchRole");
        RoleSwitcher roleSwitcherScript = roleSwitcher.GetComponent<RoleSwitcher>();
        roleSwitcherScript.canSwitch = true;
    }

    public void ActivateDynamiteQuest()
    {
        getDynamiteTextObject.SetActive(true);
        StartCoroutine(DeactivateObjectAfterSeconds(getDynamiteTextObject, 4f));
    }

    public void OnDynamitePickup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
            Debug.LogError("Player has no PlayerController");

        if (playerController.cutsceneLoad)
        {
            return;
        }

        dynamitePickupTextObject.SetActive(true);
        StartCoroutine(DeactivateObjectAfterSeconds(dynamitePickupTextObject, 3f));
        hasDynamite = true;
    }

    public void UseDynamite()
    {
        floorBreakTilemap.SetActive(false);
    }

    public void FlipLever()
    {
        bossRoomOpenedTextObject.SetActive(true);
        StartCoroutine(DeactivateObjectAfterSeconds(bossRoomOpenedTextObject, 3f));
        triggerBossLevelComponent.SetActive(true);

        bossRoomDoor.SetActive(false);
    }

    public void BossDies()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        for (float t = 0; t < 3f; t += Time.deltaTime)
        {
            float normalizedTime = t / 3f;
            // Use easing function for smoother fade
            float alpha = Mathf.Pow(normalizedTime, 2); // Start from 0 and go to 1
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1); // Set to fully opaque at the end
        SceneManager.LoadScene("WinCutscene");
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
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        if (player != null)
        {
            playerPosition = player.transform.position;
        }

        StartCoroutine(FadeAndLoadScene("BossCutscene"));
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerPosition = playerPosition;
        playerController.cutsceneLoad = true;
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
