using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallRaycastDistance = 1f; // Distance to check for walls
    [SerializeField] private float groundRaycastDistance = 0.8f; // Distance to check for ground
    private bool isTouchingWall;
    private bool isGrounded;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded and jumping
        isGrounded = IsGrounded();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Slow down the jump when the player releases the jump button
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Check for walls using raycasts
        isTouchingWall = IsTouchingWall();
    }

    private void FixedUpdate()
    {
        // Prevent movement if touching a wall and trying to move in the direction of the wall
        if (!isTouchingWall)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        // Raycast downwards to check for the ground
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayer);

        // Return true if the ray hits the ground layer
        return groundCheck.collider != null;
    }

    private bool IsTouchingWall()
    {
        // Raycast to the right and left to check for walls
        RaycastHit2D rightWallCheck = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastDistance, wallLayer);
        RaycastHit2D leftWallCheck = Physics2D.Raycast(transform.position, Vector2.left, wallRaycastDistance, wallLayer);

        if ((horizontal > 0 && rightWallCheck) || (horizontal < 0 && leftWallCheck))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the raycasts in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastDistance);

        // Visualize the ground raycast
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastDistance);
    }
}
