#region 'Using' info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }
}
