using UnityEngine;
using static PseudoMazeManager;

public class ChestEight : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        if (pseudoMazeManager.chestEight)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestEight()
    {
        if (!pseudoMazeManager.chestEight)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestEight = true;
        }
    }
}
