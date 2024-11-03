using UnityEngine;
using static JustinLevelManager;

public class LeviathanKilled : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.leviathanKilled)
        {
            gameObject.SetActive(false);
        }
    }

}
