using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

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
            audioManager.PlaySFX(audioManager.openChest);
            bridge.SetActive(true);
            pseudoMazeManager.chestSeven = true;
        }
    }
}
