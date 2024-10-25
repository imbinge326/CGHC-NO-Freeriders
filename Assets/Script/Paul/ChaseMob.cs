using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [Header("Mob Settings")]
    [SerializeField] private GameObject projectilePrefab; // Projectile prefab
    [SerializeField] private float minShootInterval = 1f; // Minimum interval between shots
    [SerializeField] private float maxShootInterval = 3f; // Maximum interval between shots
    [SerializeField] private float projectileForce = 10f; // Force applied to the projectiles

    [Header("Shooting Points")]
    [SerializeField] private Transform topShootPoint;      // Top shooting point
    [SerializeField] private Transform middleShootPoint;   // Middle shooting point
    [SerializeField] private Transform bottomShootPoint;   // Bottom shooting point

    private Animator mobAnimator;

    void Start()
    {
        // Set initial rotation to 180 degrees around the Y-axis
        transform.eulerAngles = new Vector3(0, 180, 0);

        // Get base components
        mobAnimator = GetComponent<Animator>();

        // Play Running Animation
        mobAnimator.SetBool("isMoving", true);

        // Start separate shooting coroutines for each shoot point
        StartCoroutine(ShootRandomly(topShootPoint));
        StartCoroutine(ShootRandomly(middleShootPoint));
        StartCoroutine(ShootRandomly(bottomShootPoint));
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
    }

    // Instant kill the player when within the hitbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthManager.Instance.TakeDamage(HealthManager.Instance.GetSharedHealth());
        }
    }
}
