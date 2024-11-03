using UnityEngine;
using UnityEngine.Playables;

public class DeactivateParticle : MonoBehaviour
{
    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
