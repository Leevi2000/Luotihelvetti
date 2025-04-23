using Projectile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
    [SerializeField] private int cloneCycles = 10;
    [SerializeField] private float speedIncrement = 0.1f;
    [SerializeField] private float rotateIncement = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        var childObjects = GetAllChildGameObjects(gameObject);
        float previousSpeed = childObjects[0].GetComponent<ProjectileMovement>().ForwardSpeed;
        float previousRotation = childObjects[0].GetComponent<ProjectileMovement>().BasicRotationSpeed;
        for (int i = 0; i < cloneCycles; i++) 
        {
            float increasedSpeed = previousSpeed + speedIncrement;
            float increasedRotation = previousRotation + rotateIncement;
            foreach (var obj in childObjects)
            {
                var newObj = Instantiate(obj, obj.transform.position, obj.transform.rotation, obj.transform.parent);
                newObj.GetComponent<ProjectileMovement>().ForwardSpeed = increasedSpeed;
                newObj.GetComponent<ProjectileMovement>().BasicRotationSpeed = increasedRotation;
            }
            previousSpeed = increasedSpeed;
            previousRotation = increasedRotation;
        }
    }
    List<GameObject> GetAllChildGameObjects(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child != parent.transform) // Exclude the parent itself
            {
                children.Add(child.gameObject);
            }
        }
        return children;
    }

}
