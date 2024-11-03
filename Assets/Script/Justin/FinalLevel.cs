using UnityEngine;
using static JustinLevelManager;

public class FinalLevel : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.finalChest)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
