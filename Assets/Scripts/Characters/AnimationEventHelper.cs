#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;
    public bool Melee; // Set in inspector.

    public void TriggerEvent()
    {
        if(Melee)
        {
            OnAnimationEventTriggered?.Invoke();
        }
    }

    public void TriggerAttack()
    {
        if(Melee)
        {
            OnAttackPerformed?.Invoke();
        }
    }
}