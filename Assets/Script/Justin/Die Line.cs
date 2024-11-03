using UnityEngine;
using static HealthManager;

public class DieLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instance.TakeDamage(100f);
        }
    }
}
