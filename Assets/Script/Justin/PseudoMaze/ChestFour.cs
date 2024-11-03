using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

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
            audioManager.PlaySFX(audioManager.openChest);
            movingPlatformTwo.SetActive(true);
            pseudoMazeManager.chestFour = true;
        }
    } 
}
