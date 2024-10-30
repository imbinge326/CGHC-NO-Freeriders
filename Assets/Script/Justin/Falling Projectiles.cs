using System.Collections;
using UnityEngine;

public class FallingProjectiles : MonoBehaviour
{
    private float fireballSpeed = 5f;
    private float particlesInterval = 1f;
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            HealthManager.Instance.TakeDamage(damage);
            StartCoroutine(ExplosionEffects());
        }
    }

    public IEnumerator ExplosionEffects()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(particlesInterval);
        Destroy(gameObject);
    }
}
