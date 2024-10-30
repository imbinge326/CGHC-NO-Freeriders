using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSignal : MonoBehaviour
{
    public string sceneName;
    private GameObject player;
    private GameObject roleSwitcher;

    // Boolean to determine if the player's position should be forced
    public bool forcePlayerPosition = false;

    // Position to set the player to if forcePlayerPosition is true
    public Vector3 forcedPosition;

    private void Awake()
    {
        StartCoroutine(DelayedFindPlayer());
    }

    private IEnumerator DelayedFindPlayer()
    {
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        FindPlayer();
        FindRoleSwitcher();
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

            // If forcePlayerPosition is true, set the player's position to the forcedPosition
            if (forcePlayerPosition)
            {
                player.transform.position = forcedPosition;
                Debug.Log("Player position forced to: " + forcedPosition);
            }
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
        }
    }

    private void FindRoleSwitcher()
    {
        roleSwitcher = GameObject.Find("SwitchRole");
        RoleSwitcher roleSwitcherScript = roleSwitcher.GetComponent<RoleSwitcher>();
        roleSwitcherScript.canSwitch = false;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
