using UnityEngine;

public class LeviathanAttackOne : MonoBehaviour
{
    private float interval = 0.75f;
    private float damage = 5f;
    private bool damageDealt;

    public void Start()
    {
        Destroy(gameObject, interval);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && damageDealt == false)
        {
            HealthManager.Instance.TakeDamage(damage);
            damageDealt = true;
        }
    }
}

