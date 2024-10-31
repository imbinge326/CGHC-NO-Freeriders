using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float interval = 0.25f;

    void Start()
    {
        Destroy(gameObject, interval);
    }
}
