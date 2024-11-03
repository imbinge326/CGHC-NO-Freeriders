using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f; 
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float lifetime = 5f;

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
        if (collision.CompareTag("BossAttack") || collision.CompareTag("Player") || collision.CompareTag("PlayerProjectile"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChaiEnemy enemy = collision.gameObject.GetComponent<ChaiEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
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
