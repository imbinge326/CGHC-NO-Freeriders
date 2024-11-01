using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxesTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject checkBoxesTextObject;

    private void Start()
    {
        checkBoxesTextObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkBoxesTextObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkBoxesTextObject.SetActive(false);
    }
}
