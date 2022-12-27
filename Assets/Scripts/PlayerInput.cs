#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
#endregion

// using this tutorial playlist: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField] private InputActionReference movement, attack, pointerPosition;

    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane; // will become relevant when we have a moving camera
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    { attack.action.performed += PerformAttack; }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    private void OnDisable()
    { attack.action.performed -= PerformAttack; }
}
