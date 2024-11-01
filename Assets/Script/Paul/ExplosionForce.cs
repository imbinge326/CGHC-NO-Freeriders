using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce2D : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;      // Radius of the explosion
    public float explosionForce = 700f;     // Force applied to nearby objects
    public float upwardsModifier = 1.0f;    // How much the explosion will push objects upwards
    public LayerMask playerLayer;           // Layer for detecting the player
    public Vector3 customExplosionCenter;   // Offset for the explosion center

    private void OnDestroy()
    {
        // Calculate the actual explosion center based on the custom offset
        Vector2 explosionCenter = (Vector2)transform.position + (Vector2)customExplosionCenter;

        // Find all colliders within the explosion radius from the explosion center
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionCenter, explosionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            // If the object has a Rigidbody2D, apply explosion force
            if (rb != null)
            {
                // Debugging log to ensure the player is detected
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
        // Calculate the actual explosion center based on the custom offset
        Vector2 explosionCenter = (Vector2)transform.position + (Vector2)customExplosionCenter;

        // Draw a circle in the editor to visualize the explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionCenter, explosionRadius);
    }
}
