#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#endregion

// using these tutorials: "HOW TO USE NEW INPUT SYSTEM IN UNITY" https://youtu.be/-c_LYQgG8BY
// "CREATE MELEE ATTACK / COMBAT IN UNITY P1 - ATTACK ANIMATION" https://youtu.be/7vMHTUwtyNs
// "HOW TO AIM WEAPON AT MOUSE IN UNITY" https://youtu.be/DPqc7qYDtzM

public class PlayerFloaty : MonoBehaviour
{
    [SerializeField] private InputActionReference movement, attack, pointerPosition;
    [SerializeField] private float maxSpeed = 2, acceleration = 50, deacceleration = 100;

    private float currentSpeed = 0;

    private Rigidbody2D rb;
    private WeaponParent wp;
    private PlayerAnimations playerAnimations;
    private PlayerMover playerMover;

    private Vector2 pointerInput, movementInput;

    private void OnEnable()
    { attack.action.performed += PerformAttack; }

    private void OnDisable()
    { attack.action.performed -= PerformAttack; }

    private void PerformAttack(InputAction.CallbackContext obj)
    { wp.Attack(); }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<WeaponParent>();

        playerMover = GetComponent<PlayerMover>();
        playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        wp.PointerPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>().normalized; // may not need to normalize

        //playerMover.MovementInput = movementInput;

        AnimateCharacter();
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        // mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        //playerAnimations.RotateToPointer(lookDirection);
        //playerAnimations.PlayAnimation(movementInput);
    }
}
