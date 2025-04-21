using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Enums contain set of possible actions events can have
/// </summary>
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
    public bool homing = false;

    private float time = 0f;

    public GameObject targetObj;

    [SerializeField]
    private List<MovementEvent> events;
    MovementEventActions eventActions = new MovementEventActions();

    private void Start()
    {
        if (targetObj == null)
            targetObj = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (!CheckEvents())
        {
            MoveForward();
            BasicRotation();
        }
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
        if(homing)
        {
            HomeTowardsTarget();
        }
        else
            transform.Rotate(0, 0, basicRotationSpeed * Time.deltaTime);
    }

    private bool HomeTowardsTarget()
    {
        float rotationOffset = -270;

        float deltaX, deltaY;
        eventActions.CalculateDeltaCoordinates(gameObject.transform, targetObj.transform, out deltaX, out deltaY);
        float targetZRotation = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg + rotationOffset;
        int direction = eventActions.CalculateRotateDirection(gameObject.transform.eulerAngles.z, targetZRotation);

        float speedMultiplier = (100)/(Mathf.Pow(Vector2.Distance(gameObject.transform.position, targetObj.transform.position) * Mathf.Pow(forwardSpeed, 1/4), 1));
        gameObject.transform.Rotate(0f, 0f, direction * speedMultiplier * Time.fixedDeltaTime);


        return false;
    }

    private bool CheckEvents()
    {
        bool eventsRunning = false;
        foreach (var ev in events)
        {
            ev.ProcessEvent();
            if (ev.runMethods)
            {
                ExecuteEvents(ev.methodsToRun, out ev.runMethods);
                eventsRunning = true;
            }    
        }
        return eventsRunning;
    }

    private void ExecuteEvents(List<MovementActionType> actionTypes, out bool notAllEventsCompleted)
    {
        notAllEventsCompleted = false;
        bool eventFinished = true;
        foreach (var actionType in actionTypes)
        {
            ExecuteEvent(actionType, out eventFinished);
            if (!eventFinished)
                notAllEventsCompleted = true;
        }
    }

    private void ExecuteEvent(MovementActionType actionType, out bool completed)
    {
        completed = false;
        switch (actionType)
        {
            case MovementActionType.InstantRotateTowardsPlayer:
                completed = eventActions.InstantRotateTowardsPlayer(this.gameObject, targetObj);
                break;
        }
    }
}
