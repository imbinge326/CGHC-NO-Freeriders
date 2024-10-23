using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 1)]public float smoothSpeed;
    public Vector3 offset;            

    private Transform target;

    void LateUpdate()
    {
        // Find the current player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        // If a player exists, follow the player
        if (player != null)
        {
            target = player.transform;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
