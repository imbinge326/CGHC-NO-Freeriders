using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private void OnDestroy()
    {
        FinalLevelManager.Instance.OnDynamitePickup();
    }
}
