using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // Duration of the shake effect
    public float shakeMagnitude = 0.1f; // Magnitude of the shake effect
    public float dampingSpeed = 1.0f;   // Damping speed to smooth the shake
    private Vector3 initialPosition;
    public float currentShakeDuration = 1f;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
}
