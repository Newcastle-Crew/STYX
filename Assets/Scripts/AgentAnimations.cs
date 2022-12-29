#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RotateToPointer(Vector2 lookDirection) // basically just moves the character to face whatever they need to face
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0)
        { scale.x = 2; }
        else if (lookDirection.x < 0)
        { scale.x = -2; }
        transform.localScale = scale;
    }

    public void PlayAnimation(Vector2 movementInput)
    {
        animator.SetBool("Running", movementInput.magnitude > 0);
    }
}
