using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChaseScene : MonoBehaviour
{
    private void OnDestroy()
    {
        // To prevent error when game is closed with this object still existing in the scene
        if (Application.isPlaying && gameObject.scene.isLoaded)
        {
            FinalLevelManager.Instance.SpawnChaseMob();
        }
    }
}
