using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class OpenLeftPortalDoor : MonoBehaviour
{
    public GameObject targetDoor;
    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetDoor.gameObject.SetActive(false);
            justinLevelManager.leftPortalDoor = true;
        }
    }
}
