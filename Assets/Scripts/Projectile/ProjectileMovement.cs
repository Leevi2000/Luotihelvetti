using System.Collections.Generic;
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

    private float time = 0f;

    public GameObject targetObj;

    [SerializeField]
    private List<MovementEvent> events;
    MovementEventActions eventActions = new MovementEventActions();

    private bool movementLock = false;

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
        transform.Rotate(0, 0, basicRotationSpeed * Time.deltaTime);
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
