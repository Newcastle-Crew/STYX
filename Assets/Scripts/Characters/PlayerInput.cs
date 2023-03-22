#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem; // Takes and handles input and movement for a player character
using UnityEngine.SceneManagement;
#endregion

// using this tutorial playlist: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;
    //public GameObject myUI; // formerly used to just show UI when the level was completed.

    [SerializeField] private InputActionReference movement, attack, pointerPosition;

    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    public Vector2 GetPointerInput()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            SceneManager.LoadScene("LevelSelect"); // Sends the player to the level select screen when they've beaten the level.
            //myUI.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
        }
    }
}
