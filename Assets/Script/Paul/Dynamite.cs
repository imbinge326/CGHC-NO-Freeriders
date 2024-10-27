using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private void OnDestroy()
    {
        if (Application.isPlaying && gameObject.scene.isLoaded)
        {
            FinalLevelManager.Instance.OnDynamitePickup();
        }
    }
}
