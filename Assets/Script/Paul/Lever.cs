using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Sprite flippedSprite;
    [SerializeField]
    private GameObject promptText;

    private bool flipped;

    private void Start()
    {
        promptText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (flipped)
                return;

            promptText.SetActive(true);

            if (Input.GetKey(KeyCode.T))
            {
                flipped = true;
                promptText.SetActive(false);
                SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
                spriteRend.sprite = flippedSprite;

                FinalLevelManager.Instance.FlipLever();
                SoundManager.Instance.PlaySoundEffect("LeverSFX");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        promptText.SetActive(false);
    }
}
