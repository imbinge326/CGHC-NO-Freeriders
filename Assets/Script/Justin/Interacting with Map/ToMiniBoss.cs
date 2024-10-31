using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class ToMiniBoss : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.toMiniBossDoor == true)
        {
            gameObject.SetActive(false);
        }
    }
}
