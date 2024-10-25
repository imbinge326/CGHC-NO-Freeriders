using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f; // The upward force when the player lands on the jumper

    private Animator jumperAnimator;

    private void Start()
    {
        jumperAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player landed on the jumper
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Apply an upward force to the player
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);

                // Play Animation
                jumperAnimator.SetTrigger("Jumper");
            }
        }
    }
}
