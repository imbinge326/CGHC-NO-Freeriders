using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteRoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (FinalLevelManager.Instance.hasDynamite)
            {
                FinalLevelManager.Instance.UseDynamite();
            }
            else
            {
                FinalLevelManager.Instance.ActivateDynamiteQuest();
            }
        }
    }
}
