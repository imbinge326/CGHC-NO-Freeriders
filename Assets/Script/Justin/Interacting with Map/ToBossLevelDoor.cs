using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class ToBossLevelDoor : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.toBossLevelDoor == true)
        {
            gameObject.SetActive(false);
        }
    }
}
