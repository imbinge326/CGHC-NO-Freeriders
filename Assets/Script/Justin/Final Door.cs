using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class FinalDoor : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.finalChest)
        {
            gameObject.SetActive(true);
        }
    }


}
