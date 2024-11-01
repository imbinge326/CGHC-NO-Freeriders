using UnityEngine;
using static PseudoMazeManager;

public class ChestSix : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        if (pseudoMazeManager.chestSix)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestSix()
    {
        if (!pseudoMazeManager.chestSix)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestSix = true;
        }
    }
}
