using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

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
            audioManager.PlaySFX(audioManager.openChest);
            movingPlatformOne.SetActive(true);
            pseudoMazeManager.chestTwo = true;
        }
    } 
}
