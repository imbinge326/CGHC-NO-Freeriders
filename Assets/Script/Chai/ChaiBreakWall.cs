using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaiBreakableWall : MonoBehaviour
{
    [SerializeField] private string breakerTag = "Knight"; // Reference to the prefab that can break the wall
    [SerializeField] private GameObject wallDestroyedEffect; // Optional: Effect when the wall is destroyed

    private bool isPlayerInRange = false;

    void Update()
    {
        // Check if the allowed tag is in range and the player presses the 'E' key
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

    // Detect when an object with the specified tag enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(breakerTag))
        {
            isPlayerInRange = true; // Allowed object is in range to break the wall
        }
    }

    // Detect when an object with the specified tag leaves the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(breakerTag))
        {
            isPlayerInRange = false; // Allowed object leaves, cannot break the wall
        }
    }
}