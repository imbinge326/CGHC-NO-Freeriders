using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [Header("Mob Settings")]
    [SerializeField]
    private float speed = 5f;                     // Speed of the mob
    [SerializeField]
    private GameObject projectilePrefab;            // Projectile prefab
    [SerializeField]
    private float minShootInterval = 1f;           // Minimum interval between shots
    [SerializeField]
    private float maxShootInterval = 3f;           // Maximum interval between shots
    [SerializeField]
    private float projectileForce = 10f;            // Force applied to the projectiles

    [Header("Shooting Points")]
    [SerializeField]
    private Transform topShootPoint;                // Top shooting point
    [SerializeField]
    private Transform middleShootPoint;             // Middle shooting point
    [SerializeField]
    private Transform bottomShootPoint;             // Bottom shooting point

    private Rigidbody2D rb;
    private Animator mobAnimator;

    void Start()
    {
        // Set initial rotation to 180 degrees around the Y-axis
        transform.eulerAngles = new Vector3(0, 180, 0);

        // Get base components
        rb = GetComponent<Rigidbody2D>();
        mobAnimator = GetComponent<Animator>();

        // Play Running Animation
        mobAnimator.SetBool("isMoving", true);

        // Start separate shooting coroutines for each shoot point
        StartCoroutine(ShootRandomly(topShootPoint));
        StartCoroutine(ShootRandomly(middleShootPoint));
        StartCoroutine(ShootRandomly(bottomShootPoint));
    }

    void Update()
    {
        // Move the mob to the right at a constant speed
        MoveRight();
    }

    // Move the mob right at the set speed
    void MoveRight()
    {
        rb.velocity = new Vector2(speed, 0);  // Set velocity to move right
    }

    // Coroutine to shoot at random intervals from a specific shoot point
    private IEnumerator ShootRandomly(Transform shootPoint)
    {
        while (true) // Run indefinitely
        {
            // Calculate a random interval between the minimum and maximum values
            float shootInterval = Random.Range(minShootInterval, maxShootInterval);
            yield return new WaitForSeconds(shootInterval); // Wait for the random interval

            // Shoot projectile from the specified shoot point
            ShootProjectile(shootPoint);

            // Reset attack trigger to stop animation
            mobAnimator.ResetTrigger("attack");
        }
    }

    // Shoot projectiles from the specified shooting point
    void ShootProjectile(Transform shootPoint)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned.");
            return;
        }

        // Play attacking Animation
        mobAnimator.SetTrigger("attack");

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Add a Rigidbody2D to the projectile if it doesn't already have one
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb == null)
        {
            projectileRb = projectile.AddComponent<Rigidbody2D>();
        }

        // Ensure the projectile is not affected by gravity
        projectileRb.gravityScale = 0;

        // Apply force to the right
        projectileRb.AddForce(Vector2.right * projectileForce, ForceMode2D.Impulse);

        // Change a variable on the projectile (e.g., lifetime)
        Fireball fireballScript = projectile.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.lifetime = 5;  // Set the lifetime for the fireball
        }
        else
        {
            Debug.LogError("Projectile script not found on the projectile prefab.");
        }
    }

    // Make sure the mob ignores collisions (like phasing through objects)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent the mob from stopping when colliding with other objects
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
}
