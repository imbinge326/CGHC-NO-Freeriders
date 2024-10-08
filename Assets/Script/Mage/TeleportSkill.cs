using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    [SerializeField] private string portalTag = "Portal";  // Tag for portal objects
    private Transform nearbyPortal;  // To store the reference to the nearby portal

    void Update()
    {
        // Check if the player is near a portal and presses the 'E' key
        if (nearbyPortal != null && Input.GetKeyDown(KeyCode.E))
        {
            // Teleport the player (Mage) to the other portal's position
            TeleportToOtherPortal();
        }
    }

    private void TeleportToOtherPortal()
    {
        Portal portal = nearbyPortal.GetComponent<Portal>();
        if (portal != null && portal.GetOtherPortal() != null)
        {
            transform.position = portal.GetOtherPortal().position;  // Teleport the Mage to the other portal
        }
    }

    // Detect when the Mage enters a portal's trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(portalTag))
        {
            nearbyPortal = collision.transform;  // Store the portal's reference
        }
    }

    // Detect when the Mage exits a portal's trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(portalTag))
        {
            nearbyPortal = null;  // Clear the portal reference when leaving the trigger zone
        }
    }
}
