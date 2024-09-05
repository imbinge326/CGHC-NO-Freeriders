using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    [SerializeField] private float teleportDistance = 5f;
    [SerializeField] private float teleportCooldown = 3f;
    private bool isTeleportSkillActive = false;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                Debug.Log("Teleport skill is ready to use again.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isOnCooldown)
        {
            ActivateTeleportSkill();
        }

        // Try to teleport when left-click is pressed
        if (isTeleportSkillActive && Input.GetMouseButtonDown(0))  // Left-click to teleport
        {
            TryTeleport();
        }
    }

    private void ActivateTeleportSkill()
    {
        isTeleportSkillActive = true;
        Debug.Log("Teleport skill activated! Left-click to teleport.");
    }

    private void TryTeleport()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePosition - transform.position;
        float distance = direction.magnitude;  // Calculate the distance to the mouse position

        if (distance > teleportDistance)
        {
            direction.Normalize();
            mousePosition = transform.position + direction * teleportDistance;
        }

        transform.position = mousePosition;

        // After teleporting, disable the skill and start cooldown
        isTeleportSkillActive = false;
        isOnCooldown = true;
        cooldownTimer = teleportCooldown;
        Debug.Log("Teleport skill is now on cooldown for " + teleportCooldown + " seconds.");
    }
}
