using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform otherPortal;  // Set the destination portal in the Inspector
    [SerializeField] private GameObject requiredPrefab;  // Set the specific prefab that can use the portal
    private bool isInRange = false;
    private GameObject player;  // To store the reference to the player object

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player object is of the required prefab
            if (player != null && player.CompareTag(requiredPrefab.tag))
            {
                // Teleport the player to the other portal's position
                player.transform.position = otherPortal.position;
            }
        }
    }

    // Detect when an object enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(requiredPrefab.tag))
        {
            isInRange = true;
            player = collision.gameObject;  // Store the reference to the player object
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isInRange = false;  // Disable portal use
            player = null;  // Clear the reference to the player object
        }
    }
}