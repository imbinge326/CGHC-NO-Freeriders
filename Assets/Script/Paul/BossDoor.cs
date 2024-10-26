using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject cantEnterDoorTextObject;

    private void Start()
    {
        cantEnterDoorTextObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cantEnterDoorTextObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cantEnterDoorTextObject.SetActive(false);
    }
}
