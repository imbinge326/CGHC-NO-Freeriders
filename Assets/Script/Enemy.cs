using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 100f; // Enemy's health
    //[SerializeField] private float attackPower = 10f; // Damage per attack
    [SerializeField] private float attackSpeed = 1.5f; // Time between attacks
    [SerializeField] private float attackRange = 1.5f; // Attack range
    [SerializeField] private float walkSpeed = 2f; // Walking speed
    [SerializeField] private float detectionRange = 5f; // Range to detect player
    [SerializeField] private Transform[] patrolPoints; // Points to patrol between

    private Transform player; // Reference to the player
    private int currentPatrolIndex = 0; // Current patrol point index
    private bool isChasing = false; // Is the enemy currently chasing?
    private bool isAttacking = false; // Is the enemy currently attacking?
    private float lastAttackTime = 0f; // Last attack time
    private Animator animator; // Animator for enemy animations
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                Patrol();
            }

            if (isChasing && distanceToPlayer <= attackRange && !isAttacking)
            {
                //Attack();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Patrol movement between points
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = targetPoint.position - transform.position;
        
        if (direction.sqrMagnitude < 0.1f) // Reached the patrol point
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        else
        {
            MoveTowards(direction.normalized);
        }

        if (animator != null)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isWalking", true);
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = player.position - transform.position;
        MoveTowards(direction.normalized);

        if (animator != null)
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isWalking", false);
        }
    }

    private void MoveTowards(Vector2 direction)
    {
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        rb.MovePosition(rb.position + direction * walkSpeed * Time.deltaTime);
    }

    /*private void Attack()
    {
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            lastAttackTime = Time.time;
            isAttacking = true;

            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // Apply damage to player if within range
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackPower);
            }

            StartCoroutine(ResetAttackCooldown());
        }
    }*/

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        Destroy(gameObject, 1f); // Delay destruction for death animation
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Attack range
    }
}