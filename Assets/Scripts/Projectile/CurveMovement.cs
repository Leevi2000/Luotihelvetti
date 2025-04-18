using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    public float forwardSpeed = 2f;
    public float frequency = 2f;
    public float amplitude = 0.5f;

    private float time = 0f;
    private Vector3 localStartPos;

    void Start()
    {
        localStartPos = transform.localPosition;
    }

    void Update()
    {
        time += Time.deltaTime;

        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.deltaTime;

        float verticalOffset = Mathf.Sin(time * frequency) * amplitude;
        Vector3 localOffset = transform.up * verticalOffset;

        transform.position += forwardMovement + localOffset * Time.deltaTime;
    }
}
