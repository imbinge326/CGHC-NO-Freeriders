using UnityEngine;
using UnityEngine.SceneManagement;
using static JustinLevelManager;
using static PseudoMazeManager;
using static AudioManager;

public class ChestNine : MonoBehaviour
{
    public GameObject toMiniBossWalls;
    public string cutsceneName;
    private Vector3 playerPosition;

    void Start()
    {
        if (pseudoMazeManager.chestNine)
        {
            gameObject.SetActive(false);
            toMiniBossWalls.SetActive(false);
        }
    }

    public void OpenChestNine()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player not found!!");
        }

        if (player != null)
        {
            playerPosition = player.transform.position;
        }

        if (!pseudoMazeManager.chestNine)
        {
            audioManager.PlaySFX(audioManager.openChest);
            toMiniBossWalls.SetActive(false);
            justinLevelManager.toMiniBossDoor = true;
            pseudoMazeManager.chestNine = true;
            SceneManager.LoadScene(cutsceneName);


            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.playerPosition = playerPosition;
            playerController.leviathanCutscene = true;
        }
    } 
}
