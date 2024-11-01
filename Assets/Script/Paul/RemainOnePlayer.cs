using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainOnePlayer : MonoBehaviour
{
    void Update()
    {
        // Find all objects with the "Player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // If there are more than one, keep the first one and destroy the rest
        if (players.Length > 1)
        {
            for (int i = 1; i < players.Length; i++)
            {
                Destroy(players[i]);
            }
        }
    }
}
