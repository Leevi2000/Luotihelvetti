using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGradient : MonoBehaviour
{
    // Chatgpt code

    public Gradient gradient;
    public float maxAcceleration = 2500f;
    public float minAcceleration = 0.5f;
    public float smoothFactor = 0.1f; // Lower = smoother, Higher = snappier

    private Vector3 previousPosition;
    private Vector3 previousVelocity;
    [SerializeField] private float smoothedAcceleration = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        previousPosition = transform.position;
        previousVelocity = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = (currentPosition - previousPosition) / deltaTime;

        Vector3 acceleration = (currentVelocity - previousVelocity) / deltaTime;
        float currentAcceleration = acceleration.magnitude;

        // Smooth it
        smoothedAcceleration = Mathf.Lerp(smoothedAcceleration, currentAcceleration, smoothFactor);

        // Normalize between min and max
        float t = Mathf.InverseLerp(minAcceleration, maxAcceleration, smoothedAcceleration);
        t = Mathf.Clamp01(t); // just to be safe

        // Apply color from gradient
        spriteRenderer.color = gradient.Evaluate(t);

        // Store for next frame
        previousVelocity = currentVelocity;
        previousPosition = currentPosition;
    }
}
