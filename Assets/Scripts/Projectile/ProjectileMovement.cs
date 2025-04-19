using System.Collections.Generic;
using UnityEngine;
public enum MovementActionType
{
    InstantRotateTowardsPlayer
}

public class ProjectileMovement : MonoBehaviour
{
    // General Properties for movement
    public float forwardSpeed = 2f;
    public float frequency = 2f;
    public float amplitude = 0.5f;
    public float basicRotationSpeed = 0f;

    private float time = 0f;

    public GameObject targetObj;

    [SerializeField]
    private List<MovementEvent> events;

    private bool movementLock = false;

    void FixedUpdate()
    {
        CheckEvents();
        MoveForward();
        BasicRotation();
    }

    private void MoveForward()
    {
        time += Time.deltaTime;

        Vector3 forwardMovement = transform.up * forwardSpeed * Time.deltaTime;

        float verticalOffset = Mathf.Sin(time * frequency) * amplitude;
        Vector3 localOffset = transform.right * verticalOffset;

        transform.position += forwardMovement + localOffset * Time.deltaTime;
    }

    private void BasicRotation()
    {
        transform.Rotate(0, 0, basicRotationSpeed * Time.deltaTime);
    }

    public void InstantRotateTowardsPlayer()
    {
        if (targetObj == null)
        {
            Debug.Log("Couldn't rotate projectile towards target object. Object not set");
            return;
        }

        float rotationOffset = -270;

        float deltaX, deltaY;
        CalculateDeltaCoordinates(transform, targetObj.transform, out deltaX, out deltaY);

        float targetZRotation = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg + rotationOffset;

        transform.rotation = Quaternion.Euler(0f, 0f, targetZRotation);
    }

    private void CalculateDeltaCoordinates(Transform current, Transform target, out float deltaX, out float deltaY)
    {
        deltaX = current.position.x - target.position.x;
        deltaY = current.position.y - target.position.y;
    }

    private void CheckEvents()
    {
        foreach (var ev in events)
        {
            ev.ProcessEvent();
            if (ev.runMethods)
            {
                ExecuteEvents(ev.methodsToRun);
                ev.runMethods = false;
            }    
        }
    }

    private void ExecuteEvents(List<MovementActionType> actionTypes)
    {
        foreach (var actionType in actionTypes)
            ExecuteEvent(actionType);
    }

    private void ExecuteEvent(MovementActionType actionType)
    {
        switch (actionType)
        {
            case MovementActionType.InstantRotateTowardsPlayer:
                InstantRotateTowardsPlayer();
                break;
        }
    }
}
