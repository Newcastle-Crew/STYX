#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#endregion

// using this tutorial playlist: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class PlayerAgent : MonoBehaviour
{
    private PlayerAgentAnimations agentAnimations;
    private PlayerAgentMover agentMover;
    private Oar wp;
    private PlayerWeaponParent pwp;

    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public void PerformAttack()
    { wp.Attack(); }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<PlayerAgentAnimations>();
        wp = GetComponentInChildren<Oar>();
        agentMover = GetComponent<PlayerAgentMover>();
        pwp = GetComponentInChildren<PlayerWeaponParent>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        //agentAnimations.PlayAnimation(movementInput); /// reactivate this when there's a moving animation to be played
    }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized; // may not need to normalize

        pwp.PointerPosition = pointerInput;
        agentMover.MovementInput = MovementInput;
        AnimateCharacter();
    }
}