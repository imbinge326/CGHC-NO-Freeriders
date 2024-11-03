using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMeleeAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f; // Range of the melee attack
    [SerializeField] private float attackDamage = 20f; // Damage per melee attack
    [SerializeField] private Transform attackPoint; // Reference to the position where the attack happens
    [SerializeField] private string enemyTag = "Enemy"; // Tag to identify enemies
    private bool isFacingRight = true;

    private void Update()
    {
        // Flip the knight based on the mouse position
        FlipBasedOnMouse();

        // Check for melee attack input (Left Mouse Button or E key)
        if (Input.GetMouseButtonDown(0)) // Left click to attack
        {
            MeleeAttack();
        }
    }

    // Method to handle melee attack
    private void MeleeAttack()
    {
        // Detect all colliders in range of the attack point
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Check each collider to see if it's tagged as an enemy
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag)) // Check if the object has the enemy tag
            {
                ChaiEnemy enemyScript = hitCollider.GetComponent<ChaiEnemy>(); // Get the enemy's health component
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(attackDamage); // Apply damage to the enemy
                    Debug.Log("Hit " + hitCollider.name);
                }
            }
        }
    }

    // Flip the knight based on the mouse position
    private void FlipBasedOnMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Determine if the knight should face left or right based on the mouse position
        if (mousePosition.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (mousePosition.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    // Method to flip the knight's facing direction
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the knight by scaling in the opposite direction
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Visualize the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
