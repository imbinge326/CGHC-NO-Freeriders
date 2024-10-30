using System.Collections;
using UnityEngine;

public class FallingProjectiles : MonoBehaviour
{
    private float fireballSpeed = 5f;
    private float particlesInterval = 0.5f;
    [SerializeField] private float damage;
    [SerializeField] private GameObject particles;
    void Start()
    {
        Vector2 direction = transform.right.normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * fireballSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthManager.Instance.TakeDamage(damage);
            ExplosionEffects();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            ExplosionEffects();
        }
    }

    public void ExplosionEffects()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
