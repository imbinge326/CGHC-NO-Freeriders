using UnityEngine;
using UnityEngine.SceneManagement;
using static JustinLevelManager;

public class LeviathanHealth : MonoBehaviour
{
    [SerializeField] private float leviathanCurrentHealth = 0f;
    public GameObject preChamber;
    public GameObject postChamber;
    public GameObject wallsOne;
    public GameObject wallsTwo;
    public string cutsceneName;
    private Vector3 playerPosition;


    void Start()
    {
        if (justinLevelManager.leviathanKilled)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(false);
            preChamber.SetActive(false);
            wallsOne.SetActive(false);
            wallsTwo.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        leviathanCurrentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + leviathanCurrentHealth);
        if (leviathanCurrentHealth <= 0)
        {
            justinLevelManager.leviathanKilled = true;
            justinLevelManager.miniBossDoor = true;
            gameObject.SetActive(false);
            preChamber.SetActive(false);
            wallsOne.SetActive(false);
            wallsTwo.SetActive(false);
            SceneManager.LoadScene(cutsceneName);

            var player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.LogWarning("Player not found!!");
            }

            if (player != null)
            {
                playerPosition = player.transform.position;
            }

            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.playerPosition = playerPosition;
            playerController.killLeviathanCutscene = true;
        }
    }
}
