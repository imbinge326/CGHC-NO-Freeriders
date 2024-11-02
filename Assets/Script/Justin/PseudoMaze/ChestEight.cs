using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

public class ChestEight : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        if (pseudoMazeManager.chestEight)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestEight()
    {
        if (!pseudoMazeManager.chestEight)
        {
            audioManager.PlaySFX(audioManager.openChest);
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestEight = true;
        }
    }
}
