using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviathanChangeScene : MonoBehaviour
{
    private GameObject player;
    private GameObject roleSwitcher;

    private void Awake()
    {
        StartCoroutine(DelayedFindPlayer());
    }

    private IEnumerator DelayedFindPlayer()
    {
        yield return new WaitForSeconds(0.1f); // Wait for 1 second
        FindPlayer();
    }

    private void FindPlayer()
    {
        // Find player reference in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found in scene");
            return;
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
        }
    }
}
