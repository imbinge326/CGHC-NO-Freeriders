using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class MazeDoor : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.mazeDoor == true)
        {
            gameObject.SetActive(false);
        }
    }
}
