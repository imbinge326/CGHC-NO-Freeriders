using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private bool canDealDamage = false;
    [SerializeField]
    private float colliderActiveDelay;

    private bool hasDealtDamageThisActivation = false;

    private void OnEnable()
    {
        hasDealtDamageThisActivation = false; // Reset for each activation
        StartCoroutine(CanDealDamageAfterSeconds(colliderActiveDelay));

        Animator animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
        animator.Update(0);
    }

    private void OnDisable()
    {
        canDealDamage = false;
        hasDealtDamageThisActivation = false; // Reset when the attack is deactivated
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void TryDealDamage(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDealDamage && !hasDealtDamageThisActivation)
        {
            HealthManager.Instance.TakeDamage(attackDamage);
            hasDealtDamageThisActivation = true; // Ensure only one damage per activation
        }
    }

    private IEnumerator CanDealDamageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canDealDamage = true;

        yield return new WaitForSeconds(0.3f);
        canDealDamage = false;
    }
}
