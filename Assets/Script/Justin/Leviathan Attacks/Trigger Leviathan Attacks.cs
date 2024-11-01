using UnityEngine;
using static JustinLevelManager;

public class TriggerLeviathanAttacks : MonoBehaviour
{
    public GameObject leviathanAttacks;
    public GameObject walls;

    void Start()
    {
        if (justinLevelManager.leviathanKilled)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            leviathanAttacks.SetActive(true);
            walls.SetActive(true);
        }
    }
}
