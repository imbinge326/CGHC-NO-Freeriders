using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class LeftPortalDoor : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.leftPortalDoor == true)
        {
            gameObject.SetActive(false);
        }
    }

}
