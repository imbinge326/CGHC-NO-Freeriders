using UnityEngine;
using static PseudoMazeManager;

public class ChestFive : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        if (pseudoMazeManager.chestFive)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestFive()
    {
        if (!pseudoMazeManager.chestFive)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            pseudoMazeManager.chestFive = true;
        }
    }
}
