using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
        {
            audioManager.PlaySFX(audioManager.enemyDieSound);
            Destroy(gameObject);
        }
    }
}
