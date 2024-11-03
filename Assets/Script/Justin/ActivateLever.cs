using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JustinLevelManager;

public class ActivateLever : MonoBehaviour
{
    public GameObject mazeDoor;
    public Sprite newSprite;

    public void OnLeverActivate()
    {
        mazeDoor.SetActive(false);
        justinLevelManager.mazeDoor = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
