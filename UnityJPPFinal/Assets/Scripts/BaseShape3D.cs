using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseShape3D : MonoBehaviour
{
    protected float speed = 10;

    public float Speed
    {
        get { return speed; }
        set
        {
            speed = Mathf.Clamp(value, 0f, 100f); 
        }
    }

    protected Rigidbody rigidbody;
    [SerializeField] protected Vector3 direction = Vector3.zero;
    private float arrowLength = 1;

    public bool entityDestroyed = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        direction = GetRandomDirection();
        direction.y = 0;
        direction = direction.normalized;
        Debug.Log("Direction" + direction);
    }

    void Update()
    {
        UpdateMovement();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        var position = transform.position;
        var velocity = rigidbody.velocity;

        if (velocity.magnitude < 0.1f) return;

        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(velocity), arrowLength, EventType.Repaint);
    }

    protected void UpdateMovement()
    {
        if (rigidbody.velocity.magnitude < 0.5)
        {
            UpdateRandomDirection();
        }
        rigidbody.velocity = direction * speed;
    }

    protected Vector3 GetRandomDirection()
    {
        return new Vector3(
            UnityEngine.Random.value - 0.5f,
            0,
            UnityEngine.Random.value - 0.5f
        );
    }

    protected void UpdateRandomDirection()
    {
        direction = GetRandomDirection();
        direction.y = 0;
        direction = direction.normalized;
    }

    public virtual void ActionOnCollision(Collision collision)
    {
        // Do nothing
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Entity"))
        {
            direction = Vector3.Reflect(rigidbody.velocity, collision.contacts[0].normal);
            direction.y = 0;
            direction = direction.normalized;

            rigidbody.velocity = direction * speed;
        }

        ActionOnCollision(collision);
    }

    protected void OnMouseDown()
    {
        Debug.Log("Mouse click on object: ");
    }

}
