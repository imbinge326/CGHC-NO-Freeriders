using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private string wallTag = "Wall"; 
    [SerializeField] private GameObject breakEffect;  // Optional: Effect when the wall is destroyed

    private GameObject breakableWall;

    void Update()
    {
        if (breakableWall != null && Input.GetKeyDown(KeyCode.E))
        {
            BreakWall();
        }
    }

    private void BreakWall()
    {
        if (breakEffect != null)
        {
            Instantiate(breakEffect, breakableWall.transform.position, Quaternion.identity);
        }

        Destroy(breakableWall);
        Debug.Log("Wall has been destroyed!");

        breakableWall = null;
    }

    // Detect when the Knight enters the trigger zone of a breakable wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(wallTag))
        {
            breakableWall = collision.gameObject;  // Store reference to the breakable wall
        }
    }

    // Detect when the Knight exits the trigger zone of a breakable wall
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(wallTag))
        {
            breakableWall = null;  // Clear the reference when leaving the wall's trigger zone
        }
    }
}
