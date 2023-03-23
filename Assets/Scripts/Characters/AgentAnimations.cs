#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;

    public bool isSculptor = false; // sculptors are larger, so their code is slightly different. TODO: just change pixels per unit until satisfactory size

    private void Awake()
    { animator = GetComponent<Animator>(); }

    public void RotateToPointer(Vector2 lookDirection) // basically just moves the character to face whatever they need to face
    {
        if(isSculptor == false)
        {
            Vector3 scale = transform.localScale;

            if (lookDirection.x > 0)
            { scale.x = 2; }
            else if (lookDirection.x < 0)
            { scale.x = -2; }

            transform.localScale = scale;
        }
        if(isSculptor == true)
        {
            Vector3 scale = transform.localScale;
            if (lookDirection.x > 0)
            { scale.x = 3; }
            else if (lookDirection.x < 0)
            { scale.x = -3; }
            transform.localScale = scale;
        }
    }

    public void PlayAnimation(Vector2 movementInput)
    { animator.SetBool("Running", movementInput.magnitude > 0); }
}
