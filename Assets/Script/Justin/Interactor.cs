using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public bool IsInRange;
    public UnityEvent InteractAction;
    private KeyCode InteractKey = KeyCode.F;
    public GameObject interactText;
    

    void Update()
    {
        if (IsInRange){
            if (Input.GetKeyDown(InteractKey))
            {
                InteractAction.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = false;
            if (interactText != null)
            {
                interactText.SetActive(false);
            }
        }
    }
}
