using UnityEngine;

public class InteractText : MonoBehaviour
{
    private GameObject text;

    void Start()
    {
        text = GameObject.Find("Interact Text");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }    
    }
}
