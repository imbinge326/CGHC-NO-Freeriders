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
    public bool waitBeforeMoving = false; // Option to wait before moving to the next waypoint
    public float waitTime = 1f; // Duration to wait before moving to the next waypoint

    private HashSet<Rigidbody2D> objectsOnPlatform = new HashSet<Rigidbody2D>(); // Track all objects on the platform
    private bool isWaiting = false; // Indicates whether the platform is waiting
    private float waitTimer = 0f; // Timer for waiting period

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

        // Check if the platform is waiting
        if (isWaiting)
        {
            waitTimer += Time.deltaTime; // Increment wait timer
            if (waitTimer >= waitTime) // Check if wait time has elapsed
            {
                isWaiting = false; // Stop waiting
                waitTimer = 0f; // Reset wait timer
                currentWaypointIndex = (currentWaypointIndex + 1) % globalWaypoints.Length; // Update to the next waypoint
            }
            return; // Exit Update if the platform is still waiting
        }

        // Move the platform towards the current waypoint
        Vector2 targetPosition = globalWaypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, platformSpeed * Time.deltaTime);

        // If platform reaches the current waypoint, check for waiting
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (waitBeforeMoving)
            {
                isWaiting = true; // Start waiting if the option is enabled
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % globalWaypoints.Length; // Move immediately to the next waypoint
            }
        }
    }

    private void FixedUpdate()
    {
        // Calculate platform movement delta between frames
        Vector3 platformDelta = transform.position - previousPosition;
        previousPosition = transform.position; // Update previous position for the next frame

        // Move all objects on the platform by the platform's delta
        foreach (var rb in objectsOnPlatform)
        {
            if (rb != null) // Ensure the Rigidbody2D is still valid
            {
                rb.position += (Vector2)platformDelta; // Move the object's Rigidbody2D with the platform's delta
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is tagged "Player" or "Unkillable"
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Unkillable"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                objectsOnPlatform.Add(rb); // Add the object's Rigidbody2D to the set
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the object is tagged "Player" or "Unkillable"
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Unkillable"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                objectsOnPlatform.Remove(rb); // Remove the object's Rigidbody2D from the set
            }
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
