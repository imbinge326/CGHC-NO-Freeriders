using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce2D : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;      // Radius of the explosion
    public float explosionForce = 5000f;    // Force applied to nearby objects (temporarily set high for testing)
    public float upwardsModifier = 1.0f;    // How much the explosion will push objects upwards
    public LayerMask playerLayer;           // Layer for detecting the player
    public Vector3 customExplosionCenter;   // Offset for the explosion center

    private void OnDisable()
    {
        // Calculate the actual explosion center based on the custom offset
        Vector2 explosionCenter = (Vector2)transform.position + (Vector2)customExplosionCenter;

        // Find all colliders within the explosion radius from the explosion center
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionCenter, explosionRadius, playerLayer);

        if (colliders.Length == 0)
        {
            Debug.LogWarning("No objects found within explosion radius.");
        }

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log($"Applying explosion force to: {collider.gameObject.name}");

                // Calculate the direction and force to apply
                Vector2 direction = (rb.position - explosionCenter).normalized;
                Vector2 force = direction * explosionForce;

                // Apply upward modifier to simulate explosion lift
                force.y += upwardsModifier * explosionForce;

                // Apply force to the Rigidbody2D
                rb.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning($"No Rigidbody2D found on object: {collider.gameObject.name}");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 explosionCenter = (Vector2)transform.position + (Vector2)customExplosionCenter;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionCenter, explosionRadius);
    }
}
