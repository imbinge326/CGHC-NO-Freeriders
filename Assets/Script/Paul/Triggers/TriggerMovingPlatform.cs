using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatform : MonoBehaviour
{
    [SerializeField]
    private MovingPlatform movingPlatformScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (movingPlatformScript == null)
                Debug.LogError("Moving Platform Script not found.");

            movingPlatformScript.startMoving = true;
        }
    }
}
