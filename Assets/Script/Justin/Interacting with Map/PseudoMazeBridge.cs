using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class PseudoMazeBridge : MonoBehaviour
{
    void Start()
    {
        if (justinLevelManager.pseudoMazeBridge == true)
        {
            gameObject.SetActive(false);
        }
    }
}
