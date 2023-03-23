#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class PlayerAgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] public float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField] private float currentSpeed = 0;

    float maxAcceleration = 57.5f; // highest value that acceleration can go
    float maxMaxSpeed = 5f; // highest value that maxspeed can go

    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        acceleration = 50f; // the player's default speed values.
        maxSpeed = 2f;

        acceleration = DataManager.Instance.Acceleration; // overrides the player's default speed values if they have upgrades
        maxSpeed = DataManager.Instance.MaxSpeed;

        //acceleration = DataManager.Instance.Acceleration;
        //maxSpeed = DataManager.Instance.MaxSpeed;
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

        if (acceleration > maxAcceleration && maxSpeed > maxMaxSpeed)
        {
            acceleration = maxAcceleration;
            maxSpeed = maxMaxSpeed;

            DataManager.Instance.Acceleration = maxAcceleration;
            DataManager.Instance.MaxSpeed = maxMaxSpeed;

            DataManager.Instance.SaveGame();
        }
    }
}
