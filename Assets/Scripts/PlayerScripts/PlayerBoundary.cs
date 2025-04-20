using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    [SerializeField] float KnockbackForce;
    [SerializeField] float StunTime;
    PlayerMovement playerMovement;
    float normalspeed;
    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        normalspeed = playerMovement.speed;
    }
    void OnBecameInvisible()
    {
        gameObject.GetComponentInParent<Rigidbody2D>().velocity = -transform.position.normalized * KnockbackForce;
        playerMovement.speed = 0;
        Invoke("resetSpeed", StunTime);
    }
    void resetSpeed()
    {
        playerMovement.speed = normalspeed;
    }
}
