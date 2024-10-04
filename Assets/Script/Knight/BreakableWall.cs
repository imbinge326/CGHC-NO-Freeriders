using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject allowedBreaker; // Reference to the prefab that can break the wall
    [SerializeField] private GameObject wallDestroyedEffect; // Optional: Effect when the wall is destroyed

    private bool isPlayerInRange = false;

    void Update()
    {
        // Check if the allowed prefab is in range and the player presses the 'E' key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            BreakWall();
        }
    }

    private void BreakWall()
    {
        // Play destruction effect (if any)
        if (wallDestroyedEffect != null)
        {
            Instantiate(wallDestroyedEffect, transform.position, Quaternion.identity);
        }

        // Destroy the wall
        Destroy(gameObject);
        Debug.Log("Wall has been destroyed!");
    }

    // Detect when the specified prefab enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == allowedBreaker)
        {
            isPlayerInRange = true; // Allowed object is in range to break the wall
        }
    }

    // Detect when the specified prefab leaves the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == allowedBreaker)
        {
            isPlayerInRange = false; // Allowed object leaves, cannot break the wall
        }
    }
}
