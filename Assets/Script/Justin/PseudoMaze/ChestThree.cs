using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

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
            audioManager.PlaySFX(audioManager.openChest);
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestThree = true;
        }
    }
}
