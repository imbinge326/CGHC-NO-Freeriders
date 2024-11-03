using UnityEngine;
using static JustinLevelManager;

public class PostLeviathan : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.leviathanKilled)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
