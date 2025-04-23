using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Enums contain set of possible actions events can have
/// </summary>
public enum MovementActionType
{
    InstantRotateTowardsPlayer
}

namespace Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        // General Properties for movement
        [SerializeField] private float forwardSpeed = 2f;
        [SerializeField] private float frequency = 2f;
        [SerializeField] private float amplitude = 0.5f;
        [SerializeField] private float basicRotationSpeed = 0f;
        [SerializeField] private bool homing = false;
        [SerializeField] private float homingStrengthMultiplier = 1;

        [SerializeField] private bool useModulators = true;
        [SerializeField] private bool reverseModulation = false;

        private float time = 0f;

        [SerializeField] private GameObject targetObj;

        [SerializeField]
        private List<MovementEvent> events;
        MovementEventActions eventActions = new MovementEventActions();

        [SerializeField]
        private List<Modulator.ModulatorEntry> modulatorFunctionsList;

        public float ForwardSpeed { get => forwardSpeed; set => forwardSpeed = value; }
        public float BasicRotationSpeed { get => basicRotationSpeed; set => basicRotationSpeed = value; }

        private void Start()
        {
            if (targetObj == null)
                targetObj = GameObject.FindGameObjectWithTag("Player");
        }

        void FixedUpdate()
        {
            UpdateTime();
            if (!CheckEvents())
            {
                MoveForward();
                BasicRotation();
            }
        }

        private void UpdateTime()
        {
            time += Time.deltaTime;
        }
        private void MoveForward()
        {
            Vector3 forwardMovement = transform.up * forwardSpeed * Time.deltaTime;

            float verticalOffset = Mathf.Sin(time * frequency) * amplitude;
            Vector3 localOffset = transform.right * verticalOffset;

            transform.position += forwardMovement + localOffset * Time.deltaTime;
        }

        private void BasicRotation()
        {
            if (homing)
            {
                HomeTowardsTarget();
                ApplyRotation();
            }
            else
                ApplyRotation();
        }

        private void ApplyRotation()
        {
            int directionMultiplier = 1;
            if (reverseModulation)
                directionMultiplier = -1;
            float rotationModifier = 1;
            if (modulatorFunctionsList.Count > 0)
            {
                rotationModifier = directionMultiplier * Modulator.SumOfModulatorValuesAt(modulatorFunctionsList, time);
            }

            transform.Rotate(0, 0, basicRotationSpeed * rotationModifier * Time.deltaTime);
        }

        private bool HomeTowardsTarget()
        {
            float rotationOffset = -270;

            float deltaX, deltaY;
            ProjectileOperations.CalculateDeltaCoordinates(gameObject.transform, targetObj.transform, out deltaX, out deltaY);
            float targetZRotation = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg + rotationOffset;
            int direction = ProjectileOperations.CalculateRotateDirection(gameObject.transform.eulerAngles.z, targetZRotation);

            float speedMultiplier = (100 * homingStrengthMultiplier) / (Mathf.Pow(Vector2.Distance(gameObject.transform.position, targetObj.transform.position) * Mathf.Pow(forwardSpeed, 1 / 4), 1));
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

}
