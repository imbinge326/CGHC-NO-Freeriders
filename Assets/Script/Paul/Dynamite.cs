using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying && gameObject.scene.isLoaded)
        {
            FinalLevelManager.Instance.OnDynamitePickup();
            SoundManager.Instance.PlaySoundEffect("PickupSFX");
        }
    }
}
