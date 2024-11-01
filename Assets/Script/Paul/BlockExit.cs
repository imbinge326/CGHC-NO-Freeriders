using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockExit : MonoBehaviour
{
    [SerializeField]
    private GameObject cantLeaveTextObject;

    private void Start()
    {
        cantLeaveTextObject.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cantLeaveTextObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        cantLeaveTextObject.SetActive(false);
    }
}
