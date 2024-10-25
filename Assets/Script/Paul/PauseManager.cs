using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; } // Singleton instance
    public bool IsPaused { get; private set; } = false; // Access via method

    [SerializeField]
    private GameObject pauseUI; // Reference to the GameObject to activate when paused

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
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // Toggle pause state
        }
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
}
