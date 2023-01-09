#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] public float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField] private bool isPlayer = false;
    
    float maxAcceleration = 57.5f; // highest value that acceleration can go
    float maxMaxSpeed = 5f; // highest value that maxspeed can go

    [SerializeField] private float currentSpeed = 0;

    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if(isPlayer)
        {
            acceleration = DataManager.Instance.Acceleration;
            maxSpeed = DataManager.Instance.MaxSpeed;
        }
        else if(isPlayer)
        {
            acceleration = 50f;
            maxSpeed = 2f;
        }

    }

    private void FixedUpdate()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb2d.velocity = oldMovementInput * currentSpeed;
    }

    public void upgradeSpeed()
    {
        acceleration += 2.5f;
        maxSpeed += 1f;

        DataManager.Instance.Acceleration = acceleration;
        DataManager.Instance.MaxSpeed = maxSpeed;

        if (isPlayer && acceleration > maxAcceleration && maxSpeed > maxMaxSpeed)
        {
            acceleration = maxAcceleration;
            maxSpeed = maxMaxSpeed;

            DataManager.Instance.Acceleration = maxAcceleration;
            DataManager.Instance.MaxSpeed = maxMaxSpeed;

            DataManager.Instance.SaveGame();
        }
    }
}
