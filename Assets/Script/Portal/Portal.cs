using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform otherPortal; // Set the destination portal in the Inspector
    [SerializeField] private string requiredTag = "Player"; // Set the tag for the specific role
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Check if the object has the required tag
            GameObject player = GameObject.FindWithTag(requiredTag);
            if (player != null)
            {
                // Teleport the player to the other portal's position
                player.transform.position = otherPortal.position;
            }
        }
    }

    // Detect when the player enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(requiredTag))
        {
            isPlayerInRange = true; 
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(requiredTag))
        {
            isPlayerInRange = false; // Disable portal use
        }
    }
}
