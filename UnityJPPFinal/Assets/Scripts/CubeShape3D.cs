using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShape3D : BaseShape3D
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
