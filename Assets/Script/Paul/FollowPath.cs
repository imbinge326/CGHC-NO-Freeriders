using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private bool returnToOrigin = false;

    private List<Transform> targetPoints = new List<Transform>();
    private Vector3 originalPosition;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    // Event to signal path completion
    public delegate void PathEndEventHandler();
    public event PathEndEventHandler OnPathEnd;

    void Start()
    {
        originalPosition = transform.position;

        if (targetPoints.Count > 0)
        {
            StartMovement();
        }
        else
        {
            Debug.LogWarning("No target points assigned to FollowPath.");
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
        currentTargetIndex = 0;
    }

    private void MoveTowardsTarget()
    {
        if (currentTargetIndex < targetPoints.Count)
        {
            Transform target = targetPoints[currentTargetIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentTargetIndex++;
            }
        }
        else if (returnToOrigin)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                isMoving = false;
                OnPathEnd?.Invoke(); // Invoke event on returning to origin
            }
        }
        else
        {
            isMoving = false;
            OnPathEnd?.Invoke(); // Invoke event when the end is reached
        }
    }

    public void SetTargetPoints(List<Transform> points)
    {
        targetPoints = points;
    }
}
