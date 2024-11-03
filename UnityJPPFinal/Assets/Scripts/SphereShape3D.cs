using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShape3D : BaseShape3D
{
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        UpdateRandomDirection();
    }

    void Update()
    {
        UpdateMovement();
    }

}
