using UnityEngine;
using static PseudoMazeManager;

public class ChestSeven : MonoBehaviour
{
    public GameObject bridge;

    void Start()
    {
        if (pseudoMazeManager.chestSeven)
        {
            gameObject.SetActive(false);
            bridge.SetActive(true);
        }
    }

    public void OpenChestSeven()
    {
        if (!pseudoMazeManager.chestSeven)
        {
            bridge.SetActive(true);
            pseudoMazeManager.chestSeven = true;
        }
    }
}
