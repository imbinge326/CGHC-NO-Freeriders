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

    private Vector2 facingDirection;

    void Update()
    {
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

        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.Setup(explosionRadius, explosionDamage);
        }
    }
}

