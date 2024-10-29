using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f; 
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private string targetTag = "Enemy";

    public void Setup(float radius, float damage)
    {
        explosionRadius = radius;
        explosionDamage = damage;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (targetTag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
            else if( targetTag == "Player")
            {
                HealthManager.Instance.TakeDamage(explosionDamage);
            }
        }

        Explode();
    }

    private void Explode()
    {
        // Visual explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        /*Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag(targetTag))
            {
                if (targetTag == "Enemy")
                {
                    // Get the Enemy script and apply damage
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(explosionDamage);
                    }
                }
                else if (targetTag == "Player")
                {
                    HealthManager.Instance.TakeDamage(explosionDamage);
                }
            }
        }*/

        Destroy(gameObject);  // Destroy the fireball after explosion
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected in the Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public float GetFireballDamage()
    {
        return explosionDamage;
    }
}
