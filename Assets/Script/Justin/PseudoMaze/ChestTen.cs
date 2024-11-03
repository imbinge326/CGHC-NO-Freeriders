using UnityEngine;
using static JustinLevelManager;

public class ChestTen : MonoBehaviour
{
    public GameObject activateText;
    void Start()
    {
        if (justinLevelManager.finalChest)
        {
            gameObject.SetActive(false);
            activateText.SetActive(true);
        }
    }

    public void OnChestActivated()
    {
        if(!justinLevelManager.finalChest)
        {
            activateText.SetActive(true);
            justinLevelManager.finalChest = true;
        }
    }
}
