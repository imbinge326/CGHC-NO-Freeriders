using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 5f; 
    [SerializeField]
    private bool returnToOrigin = false; // Check this if you want it to go back and forth

    private List<Transform> targetPoints = new List<Transform>(); 
    private Vector3 originalPosition; 
    private int currentTargetIndex = 0; 
    private bool isMoving = false; 

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;

        if (targetPoints.Count > 0)
        {
            StartMovement();
        }
        else
        {
            Debug.LogWarning("No target points assigned to PointMover.");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void StartMovement()
    {
        isMoving = true;
        currentTargetIndex = 0; // Start from the first target
    }

    private void MoveTowardsTarget()
    {
        if (currentTargetIndex < targetPoints.Count)
        {
            // Move towards the current target point
            Transform target = targetPoints[currentTargetIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the object has reached the target point
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentTargetIndex++; // Move to the next target
            }
        }
        else if (returnToOrigin)
        {
            // If we reached the last target, return to the original position
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);

            // Check if we have returned to the original position
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                isMoving = false; // Stop moving
            }
        }
        else
        {
            isMoving = false; // Stop moving if not returning to origin
        }
    }

    // Method to set the target points
    public void SetTargetPoints(List<Transform> points)
    {
        targetPoints = points;
    }
}
