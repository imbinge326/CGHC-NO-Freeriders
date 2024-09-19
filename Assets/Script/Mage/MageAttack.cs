using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballSpeed = 5f; 
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private SpriteRenderer spriteRenderer;  // Reference to the sprite renderer for flipping the mage
    [SerializeField] private Vector2 firePointOffsetRight = new Vector2(0.5f, 0f);  // Offset when facing right
    [SerializeField] private Vector2 firePointOffsetLeft = new Vector2(-0.5f, 0f);  // Offset when facing left

    private Vector2 facingDirection;

    void Update()
    {
        FaceMouseDirection();

        if (Input.GetMouseButtonDown(0))
        {
            FireFireball();
        }
    }

    private void FireFireball()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Make sure the Z position is set to 0 for 2D

        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Instantiate the fireball at the fire point
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Set the fireball velocity in the direction of the mouse
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * fireballSpeed;

        // Set up the fireball's explosion properties
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.Setup(explosionRadius, explosionDamage);
        }
    }

    private void FaceMouseDirection()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Ensure Z-axis is 0 for 2D

        // Determine if the mouse is to the left or right of the player
        if (mousePosition.x < transform.position.x)
        {
            // Face left by flipping the sprite horizontally and update firePoint position
            spriteRenderer.flipX = true;
            firePoint.localPosition = firePointOffsetLeft;  // Move firePoint to the left
        }
        else
        {
            // Face right (default direction) and update firePoint position
            spriteRenderer.flipX = false;
            firePoint.localPosition = firePointOffsetRight;  // Move firePoint to the right
        }
    }
}

