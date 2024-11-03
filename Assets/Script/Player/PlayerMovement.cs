using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallRaycastDistance = 1f; // Distance to check for walls
    [SerializeField] private float groundRaycastDistance = 0.5f; // Distance to check for ground
    [SerializeField] private float edgeOffset = 0.5f; // Offset for edge checks

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
            audioManager.PlaySFX(audioManager.jumping);
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
        if (isGrounded && isTouchingWall)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else if (!isTouchingWall)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        // Raycast downwards from the center, left edge, and right edge to check for the ground
        Vector2 center = transform.position;
        Vector2 leftEdge = new Vector2(center.x - edgeOffset, center.y);
        Vector2 rightEdge = new Vector2(center.x + edgeOffset, center.y);

        RaycastHit2D centerGroundCheck = Physics2D.Raycast(center, Vector2.down, groundRaycastDistance, groundLayer);
        RaycastHit2D leftGroundCheck = Physics2D.Raycast(leftEdge, Vector2.down, groundRaycastDistance, groundLayer);
        RaycastHit2D rightGroundCheck = Physics2D.Raycast(rightEdge, Vector2.down, groundRaycastDistance, groundLayer);

        // Return true if any of the raycasts hit the ground
        return centerGroundCheck.collider != null || leftGroundCheck.collider != null || rightGroundCheck.collider != null;
    }

    private bool IsTouchingWall()
    {
        // Raycast to the right and left from the top and bottom edges to check for walls
        Vector2 center = transform.position;
        Vector2 topEdge = new Vector2(center.x, center.y + edgeOffset);
        Vector2 bottomEdge = new Vector2(center.x, center.y - edgeOffset);

        RaycastHit2D rightWallTopCheck = Physics2D.Raycast(topEdge, Vector2.right, wallRaycastDistance, groundLayer);
        RaycastHit2D rightWallBottomCheck = Physics2D.Raycast(bottomEdge, Vector2.right, wallRaycastDistance, groundLayer);
        RaycastHit2D leftWallTopCheck = Physics2D.Raycast(topEdge, Vector2.left, wallRaycastDistance, groundLayer);
        RaycastHit2D leftWallBottomCheck = Physics2D.Raycast(bottomEdge, Vector2.left, wallRaycastDistance, groundLayer);

        // Return true if any of the horizontal raycasts hit the wall layer
        return (horizontal > 0 && (rightWallTopCheck || rightWallBottomCheck)) ||
               (horizontal < 0 && (leftWallTopCheck || leftWallBottomCheck));
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 center = transform.position;
        Vector2 leftEdge = new Vector2(center.x - edgeOffset, center.y);
        Vector2 rightEdge = new Vector2(center.x + edgeOffset, center.y);
        Vector2 topEdge = new Vector2(center.x, center.y + edgeOffset);
        Vector2 bottomEdge = new Vector2(center.x, center.y - edgeOffset);

        // Visualize the ground raycasts
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + Vector2.down * groundRaycastDistance);
        Gizmos.DrawLine(leftEdge, leftEdge + Vector2.down * groundRaycastDistance);
        Gizmos.DrawLine(rightEdge, rightEdge + Vector2.down * groundRaycastDistance);

        // Visualize the wall raycasts
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topEdge, topEdge + Vector2.right * wallRaycastDistance);
        Gizmos.DrawLine(bottomEdge, bottomEdge + Vector2.right * wallRaycastDistance);
        Gizmos.DrawLine(topEdge, topEdge + Vector2.left * wallRaycastDistance);
        Gizmos.DrawLine(bottomEdge, bottomEdge + Vector2.left * wallRaycastDistance);
    }
}
