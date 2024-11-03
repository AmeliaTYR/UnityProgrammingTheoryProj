using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShape3D : BaseShape3D
{
    public ParticleSystem collisionEffect;
    public float collisionForceThreshold = 5f;
    public EntitySpawner entitySpawner;

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
        if (!collision.collider.CompareTag("Entity"))
        {
            return;
        }

        // Calculate the collision force magnitude
        float collisionForce = collision.relativeVelocity.magnitude;

        // Check if the force is above the threshold
        if (collisionForce >= collisionForceThreshold)
        {
            BaseShape3D baseShape3D = collision.gameObject.GetComponent<BaseShape3D>();
            if (!baseShape3D.entityDestroyed)
            {
                baseShape3D.entityDestroyed = true;

                Destroy(collision.gameObject);
                entitySpawner.EntityDestroyed();

                // Play the particle effect if it's not already playing
                if (collisionEffect != null && !collisionEffect.isPlaying)
                {
                    collisionEffect.transform.position = collision.contacts[0].point; // Set effect position to collision point
                    collisionEffect.Play();
                }
            }

        }
    }

}
