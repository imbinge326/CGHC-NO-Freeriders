using System.Collections;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze everything
            }
            else
            {
                Debug.LogError("Rigidbody2D not found on Player");
            }

            FinalLevelManager.Instance.TriggerBossFight();
        }
    }
}
