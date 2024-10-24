using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChaseScene : MonoBehaviour
{
    private void OnDestroy()
    {
        FinalLevelManager.Instance.SpawnChaseMob();
    }
}
