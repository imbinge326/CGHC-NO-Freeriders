using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class FinalChest : MonoBehaviour
{
    public GameObject hiddenText;
    void Start()
    {
        if (justinLevelManager.finalChest)
        {
            hiddenText.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void OpenChest()
    {
        if (!justinLevelManager.finalChest)
        {
            hiddenText.SetActive(true);
            justinLevelManager.finalChest = true;
        }
    }
}
