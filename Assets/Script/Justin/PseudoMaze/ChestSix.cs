using UnityEngine;
using static PseudoMazeManager;
using static AudioManager;

public class ChestSix : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        if (pseudoMazeManager.chestSix)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenChestSix()
    {
        if (!pseudoMazeManager.chestSix)
        {
            audioManager.PlaySFX(audioManager.openChest);
            Instantiate(enemy, transform.position, Quaternion.identity);
            pseudoMazeManager.chestSix = true;
        }
    }
}
