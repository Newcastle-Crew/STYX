#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#endregion

// using this tutorial playlist: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    private Vector2 pointerInput, movementInput;

    private WeaponParent wp;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public void PerformAttack()
    { wp.Attack(); }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        wp = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized; // may not need to normalize

        wp.PointerPosition = pointerInput;

        agentMover.MovementInput = MovementInput;

        AnimateCharacter();
    }
}