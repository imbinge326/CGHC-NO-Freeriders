using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class MiniBossDoor : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.miniBossDoor == true)
        {
            gameObject.SetActive(false);
        }
    }
}
