using UnityEngine;
using static PseudoMazeManager;

public class ChestFour : MonoBehaviour
{
    public GameObject movingPlatformTwo;

    void Start()
    {
        if (pseudoMazeManager.chestFour)
        {
            gameObject.SetActive(false);
            movingPlatformTwo.SetActive(true);
        }
    }

    public void OpenChestFour()
    {
        if (!pseudoMazeManager.chestFour)
        {
            movingPlatformTwo.SetActive(true);
            pseudoMazeManager.chestFour = true;
        }
    } 
}
