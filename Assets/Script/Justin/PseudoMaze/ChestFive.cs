using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

public class ChestFive : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        if (pseudoMazeManager.chestFive)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestFive()
    {
        if (!pseudoMazeManager.chestFive)
        {
            audioManager.PlaySFX(audioManager.openChest);
            Instantiate(explosion, transform.position, Quaternion.identity);
            pseudoMazeManager.chestFive = true;
        }
    }
}
