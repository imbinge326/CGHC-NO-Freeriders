using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    [SerializeField]
    private float bossHealth = 1000f;

    public bool isVulnerable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") && isVulnerable)
        {
            Fireball fireball = collision.GetComponent<Fireball>();
            if (fireball == null)
                Debug.LogError("Fireball script not found");

            bossHealth -= fireball.GetFireballDamage();

            CheckBossHealth(bossHealth);
        }
    }

    void CheckBossHealth(float bossHealth)
    {
        if (bossHealth <= 0)
        {
            FinalBossManager.Instance.BossDies();
        }
    }

    public float GetBossHealth()
    {
        return bossHealth;
    }
}
