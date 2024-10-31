using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; } // Singleton instance
    public bool IsPaused { get; private set; } = false; // Access via method

    [SerializeField]
    private GameObject pauseUI;

    // List of scene names where pausing is disabled
    [SerializeField]
    private List<string> cutsceneScenes; // Add your cutscene scene names here

    private void Awake()
    {
        // Ensure that only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
    }

    private void Update()
    {
        // Check for Escape key press only if pausing is allowed in this scene
        if (Input.GetKeyDown(KeyCode.Escape) && CanPause())
        {
            TogglePause();
        }
    }

    // Method to check if pausing is allowed in the current scene
    private bool CanPause()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return !cutsceneScenes.Contains(currentSceneName);
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            Time.timeScale = 0; // Pause the game
            ActivatePauseMenu(); // Activate the GameObject when paused
        }
        else
        {
            Time.timeScale = 1; // Resume the game
            DeactivatePauseMenu(); // Deactivate the GameObject when resuming
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ActivatePauseMenu()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(true); // Activate the GameObject
        }
    }

    private void DeactivatePauseMenu()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false); // Deactivate the GameObject
        }
    }

    private IEnumerator DelayedFindPauseUI()
    {
        yield return new WaitForSeconds(1f);
        pauseUI = GameObject.Find("PauseUI");
        if (pauseUI == null)
            Debug.LogError("PauseUI not found");

        pauseUI.SetActive(false);
    }
}
