using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteRoomTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject promptText;
    private bool hasInteracted = false; // Tracks if the player has already interacted

    private void Start()
    {
        promptText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted)
        {
            promptText.SetActive(true);

            if (Input.GetKey(KeyCode.T))
            {
                if (FinalLevelManager.Instance.hasDynamite)
                {
                    FinalLevelManager.Instance.UseDynamite();
                    SoundManager.Instance.PlaySoundEffect("FloorBreakSFX");

                    gameObject.SetActive(false);
                }
                else
                {
                    FinalLevelManager.Instance.ActivateDynamiteQuest();
                }

                promptText.SetActive(false);
                hasInteracted = true; // Set to true after interaction
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            promptText.SetActive(false);

            hasInteracted = false; // Reset the interaction flag when player exits
        }
    }
}
