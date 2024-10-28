using System.Collections;
using UnityEngine;

public class FallingFireball : MonoBehaviour
{
    private Fireball fireBall;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed = 5f; 
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private float interval = 5f;

    public void Awake()
    {
        fireBall = fireballPrefab.AddComponent<Fireball>();
        StartCoroutine(FireballSpawnCycle());
    }

    public IEnumerator FireballSpawnCycle()
    {
        while (true)
        {
            FireFireball();
            yield return new WaitForSeconds(interval);
        }
    }
    private void FireFireball()
    {
        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = -transform.up.normalized;

        // Instantiate the fireball at the fire point
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Set the fireball velocity in the direction of the mouse
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * fireballSpeed;

        if (fireBall != null)
        {
            fireBall.Setup(explosionRadius, explosionDamage);
        }
    }
}
