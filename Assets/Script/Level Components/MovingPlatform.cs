using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2[] localWaypoints; // Coordinates of the waypoints relative to the platform's start position
    private Vector2[] globalWaypoints; // World-space coordinates of the waypoints
    [SerializeField] private float platformSpeed = 2f; // Speed of platform movement
    private int currentWaypointIndex = 0; // Index to track which waypoint to move towards
    private Vector3 previousPosition; // Store the platform's previous position for calculating delta

    public bool startMoving = false; // Control to start/stop platform movement

    private bool playerIsOnPlatform = false; // Is the player standing on the platform
    private GameObject player; // Reference to the player GameObject
    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D

    private void Start()
    {
        // Initialize previous position
        previousPosition = transform.position;

        // Convert local waypoints to global positions at the start
        globalWaypoints = new Vector2[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = transform.TransformPoint(localWaypoints[i]);
        }
    }

    private void Update()
    {
        if (!startMoving || globalWaypoints.Length == 0) return; // Only move if startMoving is true

        // Move the platform towards the current waypoint
        Vector2 targetPosition = globalWaypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, platformSpeed * Time.deltaTime);

        // If platform reaches the current waypoint, update to the next waypoint
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % globalWaypoints.Length; // Loop back to the first waypoint after reaching the last one
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

        // Convert and draw waypoints as if they are local to the platform, but in world space
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            Vector3 globalWaypointPosition = Application.isPlaying ? globalWaypoints[i] : transform.TransformPoint(localWaypoints[i]);
            Gizmos.DrawSphere(globalWaypointPosition, 0.2f);
        }
    }
}
