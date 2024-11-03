using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShape3D : BaseShape3D
{
    public ParticleSystem collisionEffect;
    public float collisionForceThreshold = 5f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        UpdateRandomDirection();
    }

    void Update()
    {
        UpdateMovement();
    }

    public override void ActionOnCollision(Collision collision)
    {
        // Calculate the collision force magnitude
        float collisionForce = collision.relativeVelocity.magnitude;

        // Check if the force is above the threshold
        if (collisionForce >= collisionForceThreshold)
        {
            // Play the particle effect if it's not already playing
            if (collisionEffect != null && !collisionEffect.isPlaying)
            {
                collisionEffect.transform.position = collision.contacts[0].point; // Set effect position to collision point
                collisionEffect.Play();
            }
        }
    }

}
