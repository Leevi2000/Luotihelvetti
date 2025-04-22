using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Projectile
{
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
            ProjectileOperations.CalculateDeltaCoordinates(objectToRotate.transform, targetObject.transform, out deltaX, out deltaY);

            float targetZRotation = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg + rotationOffset;


            int direction = ProjectileOperations.CalculateRotateDirection(objectToRotate.transform.eulerAngles.z, targetZRotation);

            if (ProjectileOperations.FacingTowardsTarget(objectToRotate.transform.eulerAngles.z, targetZRotation))
                return true;

            float speedMultiplier = Mathf.Abs(ProjectileOperations.CalculateDeltaDegrees(objectToRotate.transform.eulerAngles.z, targetZRotation));
            objectToRotate.transform.Rotate(0f, 0f, direction * smoothedRotationSpeed * speedMultiplier * Time.fixedDeltaTime);

            return false;
        }



        public bool SetHoming()
        {
            return false;
        }

 

     
    }

}
