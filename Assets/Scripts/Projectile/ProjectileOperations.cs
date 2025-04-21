using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public static class ProjectileOperations
    {

        /// <summary>
        /// Returns 1 for clockwise rotation and -1 for counterclockwise.
        /// </summary>
        /// <param name="currentZRotation"></param>
        /// <param name="targetZRotation"></param>
        /// <returns></returns>
        public static int CalculateRotateDirection(float currentZRotation, float targetZRotation)
        {
            float deltaDegrees = NormalizeToSignedDegrees(targetZRotation - currentZRotation);
            if (deltaDegrees >= 0)
                return 1;
            else
                return -1;
        }

        public static float CalculateDeltaDegrees(float currentZRotation, float targetZRotation, bool clockwise = true)
        {
            float delta = NormalizeToSignedDegrees(targetZRotation - currentZRotation);
            return Mathf.Abs(delta);
        }

        public static float NormalizeToSignedDegrees(float degrees)
        {
            float result = (degrees + 180) % 360;
            if (result < 0) result += 360;
            return result - 180;
        }

        public static void CalculateDeltaCoordinates(Transform current, Transform target, out float deltaX, out float deltaY)
        {
            deltaX = current.position.x - target.position.x;
            deltaY = current.position.y - target.position.y;
        }

        public static bool FacingTowardsTarget(float currentZRotation, float targetZRotation)
        {
            float accuracy = 10f;

            float deltaDegrees = CalculateDeltaDegrees(currentZRotation, targetZRotation);

            if (deltaDegrees < accuracy)
                return true;

            return false;
        }
    }
}

