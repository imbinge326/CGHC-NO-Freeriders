using System.Collections;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    private GameObject roleSwitcher;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                roleSwitcher = GameObject.Find("SwitchRole");
                RoleSwitcher roleSwitcherScript = roleSwitcher.GetComponent<RoleSwitcher>();
                roleSwitcherScript.canSwitch = false;
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                Debug.LogError("Rigidbody2D not found on Player");
            }

            FinalLevelManager.Instance.TriggerBossFight();
        }
    }
}
