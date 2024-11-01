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
        }
    }
}
