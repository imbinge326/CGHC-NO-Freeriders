using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

public class ChestOne : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        if (pseudoMazeManager.chestOne)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestOne()
    {
        if (!pseudoMazeManager.chestOne)
        {
            audioManager.PlaySFX(audioManager.openChest);
            Instantiate(explosion, transform.position, Quaternion.identity);
            pseudoMazeManager.chestOne = true;
        }
    }
}
