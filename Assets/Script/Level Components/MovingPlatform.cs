using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2[] waypoints; // Coordinates the platform will move between
    [SerializeField] private float platformSpeed = 2f; // Speed of platform movement
    private int currentWaypointIndex = 0; // Index to track which waypoint to move towards
    private Vector3 previousPosition; // Store the platform's previous position for calculating delta

    private bool playerIsOnPlatform = false; // Is the player standing on the platform
    private GameObject player; // Reference to the player GameObject
    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D

    private void Start()
    {
        previousPosition = transform.position; // Initialize previous position
    }

    private void Update()
    {
        if (waypoints.Length == 0) return;

        // Move the platform towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex], platformSpeed * Time.deltaTime);

        // If platform reaches the current waypoint, update to the next waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint after reaching the last one
        }
    }

    private void FixedUpdate()
    {
        // Calculate platform movement delta between frames
        Vector3 platformDelta = transform.position - previousPosition;
        previousPosition = transform.position; // Update previous position for the next frame

        // If the player is on the platform, move them by the platform's delta
        if (playerIsOnPlatform && player != null)
        {
            // Move the player's Rigidbody2D with the platform's delta
            playerRb.position += (Vector2)platformDelta;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnPlatform = true;
            player = collision.gameObject;
            playerRb = player.GetComponent<Rigidbody2D>(); // Get the player's Rigidbody2D
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOnPlatform = false;
            player = null;
            playerRb = null; // Clear the reference to the player's Rigidbody2D
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the waypoints in the Scene view
        Gizmos.color = Color.yellow;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i], 0.2f);
        }
    }
}
