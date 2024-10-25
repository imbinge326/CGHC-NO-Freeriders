using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMob : MonoBehaviour
{
    private Animator mobAnimator;
    private FollowPath followPathScript;

    void Start()
    {
        // Set initial rotation to 180 degrees around the Y-axis
        transform.eulerAngles = new Vector3(0, 180, 0);

        // Get base components
        mobAnimator = GetComponent<Animator>();
        followPathScript = GetComponent<FollowPath>();

        // Play Running Animation
        mobAnimator.SetBool("isMoving", true);

        // Subscribe to event
        followPathScript.OnPathEnd += PlayIdleAnimation;
    }

    // Instant kill the player when within the hitbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthManager.Instance.TakeDamage(HealthManager.Instance.GetSharedHealth());
        }
    }

    private void PlayIdleAnimation()
    {
        mobAnimator.SetBool("isMoving", false);
    }

    private void OnDestroy()
    {
        followPathScript.OnPathEnd -= PlayIdleAnimation;
    }
}
