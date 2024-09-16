using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health = 100f; 
    private float currentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // You can destroy the object or trigger an animation here
        Destroy(gameObject);
    }

    // Method to return the current health (if needed for other scripts)
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
