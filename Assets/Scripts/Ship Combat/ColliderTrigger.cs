#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerInput player = collider.GetComponent<PlayerInput>();

        if(player != null) // player is inside the 'start the level' area (near the wheel)
        {
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
