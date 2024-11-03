using UnityEngine;
using System.Collections;

public class ChaiEnemy : MonoBehaviour
{
    public float hp = 100; // Enemy health
    public float attackSpeed = 1.5f; // Attack cooldown
    public float attackPower = 10; // Attack power
    public float attackRange = 1f; // Attack range
    public float walkSpeed = 2f; // Walking speed
    public float detectionRange = 5f; // Detection distance

    private Transform player; // Player object
    [SerializeField]
    private bool isChasing = false; // Is chasing the player
    [SerializeField]
    private bool isAttacking = false; // Is attacking
    private float lastAttackTime = 0f; // Last attack time
    private Animator animator; // For playing animations

    void Start()
    {
        // Get Animator component (if any)
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Dynamically find Player object (search each frame)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        // If player object is not found, return
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(player.position, transform.position); // Use actual distance

        // Check if the player is invisible
        Invisible playerInvisible = player.GetComponent<Invisible>();

        // If the player is within detection range and not invisible, chase the player
        if (distanceToPlayer <= detectionRange && (playerInvisible == null || !playerInvisible.isInvisible) && !isAttacking)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            // Player is out of range or invisible, stop chasing
            isChasing = false;

            // Stop chase animation
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }

        // If the player is within attack range and chasing, attack
        if (isChasing && distanceToPlayer <= attackRange && !isAttacking)
        {
            Debug.Log("Attacking!!!!!!!!!!");
            AttackPlayer();
        }
    }

    // Chase player logic
    void ChasePlayer()
    {
        // Move towards the player
        if (player.position.x < transform.position.x)
        {
            // Player is to the left, move left
            transform.position += Vector3.left * walkSpeed * Time.deltaTime;
            FlipSprite(true); // Flip to face left
        }
        else if (player.position.x > transform.position.x)
        {
            // Player is to the right, move right
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            FlipSprite(false); // Flip to face right
        }

        // Play chase animation (if any)
        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }
    }

    // Flip the sprite by rotating around the Y-axis
    private void FlipSprite(bool facingRight)
    {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        // Set the rotation
        transform.rotation = targetRotation;
    }

    // Attack player logic
    void AttackPlayer()
    {
        // Check attack cooldown
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            // Play attack animation (if any)
            if (animator != null)
            {
                animator.SetTrigger("attack"); // Ensure your animator has an "Attack" trigger
            }

            // Mark as attacking to prevent repeated attacks
            isAttacking = true;

            // Deduct health from the player (assume player has a HealthManager)
            HealthManager.Instance.TakeDamage(attackPower);

            // Record the time of the attack
            lastAttackTime = Time.time;

            // Start cooldown timer, 1.5 seconds before able to attack again
            StartCoroutine(AttackCooldown());
            
        }
    }

    // Attack cooldown logic
    IEnumerator AttackCooldown()
    {
        // Wait for attack cooldown time to end
        yield return new WaitForSeconds(attackSpeed);

        // Reset attack state
        isAttacking = false;
    }

    // Enemy takes damage
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    // Enemy death logic
    void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invisible playerInvisible = collision.gameObject.GetComponent<Invisible>();
            if (playerInvisible != null && playerInvisible.isInvisible)
            {
                // Player is invisible, ignore collision
                return;
            }

            // Execute other collision logic
        }
    }

    // Visualize detection and attack ranges
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Attack range
    }
}
