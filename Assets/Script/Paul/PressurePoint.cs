using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePoint : MonoBehaviour
{
    [SerializeField] private GameObject DoorToUnlock;
    [SerializeField] private Vector2 positionToMoveTo = new Vector2(0, 2);
    [SerializeField] private float duration = 2f; // Duration of the movement

    private Vector3 doorStartPosition;
    private Vector3 doorTargetPosition;
    private float elapsedTime;
    private bool isMovingToTarget = false;
    private bool isMovingBack = false;

    Animator animator;

    private void Start()
    {
        if (DoorToUnlock != null)
        {
            // Store the door's starting position and calculate the target position relative to it
            doorStartPosition = DoorToUnlock.transform.position;
            doorTargetPosition = doorStartPosition + (Vector3)positionToMoveTo;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator not found");
    }

    private void Update()
    {
        if (isMovingToTarget && DoorToUnlock != null)
        {
            MoveDoor(DoorToUnlock.transform.position, doorTargetPosition);
        }
        else if (isMovingBack && DoorToUnlock != null)
        {
            MoveDoor(DoorToUnlock.transform.position, doorStartPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Unkillable") && !isMovingToTarget)
        {
            elapsedTime = 0f;         // Reset elapsed time
            isMovingToTarget = true;  // Start moving the door to the target position
            isMovingBack = false;     // Ensure we're not moving back
            animator.SetBool("isTriggered", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unkillable") && !isMovingBack)
        {
            elapsedTime = 0f;        // Reset elapsed time
            isMovingBack = true;     // Start moving the door back to the start
            isMovingToTarget = false; // Ensure we're not moving to the target
            animator.SetBool("isTriggered", false);
        }
    }

    private void MoveDoor(Vector3 startPosition, Vector3 endPosition)
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);
        DoorToUnlock.transform.position = Vector2.Lerp(startPosition, endPosition, t);

        // Stop moving once we reach the target or start position
        if (t >= 1f)
        {
            isMovingToTarget = false;
            isMovingBack = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (DoorToUnlock != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(DoorToUnlock.transform.position, 0.1f);

            Vector3 targetPosition = DoorToUnlock.transform.position + (Vector3)positionToMoveTo;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition, 0.1f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(DoorToUnlock.transform.position, targetPosition);
        }
    }
}
