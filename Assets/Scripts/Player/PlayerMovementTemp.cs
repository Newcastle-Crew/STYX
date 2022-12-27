#region 'using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Takes and handles input and movement for a player character
#endregion

public class PlayerMovementTemp : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public GameObject myUI;

    bool canMove = true;
    
    void Start() // Start is called before the first frame update
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {         
            if (GetMovement() != Vector2.zero) // If movement input is not 0, try to move
            {               
                bool success = TryMove(GetMovement()); //bool success = TryMove(movementInput);

                if (!success)
                { success = TryMove(new Vector2(GetMovement().x, 0)); }

                if (!success)
                { success = TryMove(new Vector2(0, GetMovement().y)); }

                animator.SetBool("isMoving", success);
            }
            else
            { animator.SetBool("isMoving", false); }
        
            if (movementInput.x < 0) // Set direction of sprite to movement direction
            { spriteRenderer.flipX = true; }
            else if (movementInput.x > 0)
            { spriteRenderer.flipX = false; }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            { return false; }
        }
        else
        { return false; } // Can't move if there's no direction to move in
    }

    Vector2 GetMovement()
    {
        Vector2 output = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (output.sqrMagnitude > 1)
            output.Normalize();

        return output;
    }

    //void OnMove(InputValue movementValue)
    //{
    //    movementInput = movementValue.Get<Vector2>();
    //}

    void OnFire()
    { animator.SetTrigger("swordAttack"); }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        { swordAttack.AttackLeft(); }
        else
        { swordAttack.AttackRight(); }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    { canMove = false; }

    public void UnlockMovement()
    { canMove = true; }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Exit")
        {
            //SceneManager.LoadScene("GameScene"); // Lets the player fire or rotate the cannon when they're in the right spot.
            myUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void NoMoreMouse()
    { Cursor.lockState = CursorLockMode.Locked; }

    public void upgradeSpeed()
    {
        moveSpeed += 2.05f;
    }
}