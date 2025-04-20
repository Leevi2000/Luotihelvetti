using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEventActions
{

    /// <summary>
    /// Rotates given object towards target an set amount per iteration. Returns true when rotated towards target.
    /// </summary>
    /// <param name="objectToRotate"></param>
    /// <param name="targetObject"></param>
    /// <returns></returns>
    public bool InstantRotateTowardsPlayer(GameObject objectToRotate, GameObject targetObject)
    {
        float smoothedRotationSpeed = 10;

        if (targetObject == null)
        {
            Debug.Log("Couldn't rotate projectile towards target object. Object not set");
            return false;
        }

        float rotationOffset = -270;

        float deltaX, deltaY;
        CalculateDeltaCoordinates(objectToRotate.transform, targetObject.transform, out deltaX, out deltaY);

        float targetZRotation = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg + rotationOffset;


        int direction = CalculateRotateDirection(objectToRotate.transform.eulerAngles.z, targetZRotation);

        if (FacingTowardsTarget(objectToRotate.transform.eulerAngles.z, targetZRotation))
            return true;

        float speedMultiplier = Mathf.Abs(CalculateDeltaDegrees(objectToRotate.transform.eulerAngles.z, targetZRotation));
        objectToRotate.transform.Rotate(0f, 0f, direction * smoothedRotationSpeed * speedMultiplier * Time.fixedDeltaTime);

        return false;
    }

   

    public bool SetHoming()
    {
        return false;
    }


    /// <summary>
    /// Returns 1 for clockwise rotation and -1 for counterclockwise.
    /// </summary>
    /// <param name="currentZRotation"></param>
    /// <param name="targetZRotation"></param>
    /// <returns></returns>
    public int CalculateRotateDirection(float currentZRotation, float targetZRotation)
    {
        float deltaDegrees = NormalizeToSignedDegrees(targetZRotation - currentZRotation);
        if (deltaDegrees >= 0)
            return 1;
        else
            return -1;
    }

    public float CalculateDeltaDegrees(float currentZRotation, float targetZRotation, bool clockwise = true)
    {
        float delta = NormalizeToSignedDegrees(targetZRotation - currentZRotation);
        return Mathf.Abs(delta);
    }

    private float NormalizeToSignedDegrees(float degrees)
    {
        float result = (degrees + 180) % 360;
        if (result < 0) result += 360;
        return result - 180;
    }
    public void CalculateDeltaCoordinates(Transform current, Transform target, out float deltaX, out float deltaY)
    {
        deltaX = current.position.x - target.position.x;
        deltaY = current.position.y - target.position.y;
    }

    private bool FacingTowardsTarget(float currentZRotation, float targetZRotation)
    {
        float accuracy = 10f;

        float deltaDegrees = CalculateDeltaDegrees(currentZRotation, targetZRotation);

        if (deltaDegrees < accuracy)
            return true;

        return false;
    }
}
