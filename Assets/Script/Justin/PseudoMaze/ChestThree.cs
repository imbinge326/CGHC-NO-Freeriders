using UnityEngine;
using static PseudoMazeManager;

public class ChestThree : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        if (pseudoMazeManager.chestThree)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestThree()
    {
        if (!pseudoMazeManager.chestThree)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestThree = true;
        }
    }
}
