using UnityEngine;
using static PseudoMazeManager;

public class ChestOne : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        if (pseudoMazeManager.chestOne)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestOne()
    {
        if (!pseudoMazeManager.chestOne)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            pseudoMazeManager.chestOne = true;
        }
    }
}
