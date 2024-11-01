using UnityEngine;
using static PseudoMazeManager;

public class ChestTwo : MonoBehaviour
{
    public GameObject movingPlatformOne;

    void Start()
    {
        if (pseudoMazeManager.chestTwo)
        {
            gameObject.SetActive(false);
            movingPlatformOne.SetActive(true);
        }
    }

    public void OpenChestTwo()
    {
        if (!pseudoMazeManager.chestTwo)
        {
            movingPlatformOne.SetActive(true);
            pseudoMazeManager.chestTwo = true;
        }
    } 
}
