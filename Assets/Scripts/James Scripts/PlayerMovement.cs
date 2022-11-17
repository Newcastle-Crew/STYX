using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpForce = 6f;

    public delegate void Attacked();
    public static Attacked attacked;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        rb.velocity = new Vector3(direction.x * 5f, rb.velocity.y, direction.y * 5f);

        print(rb.velocity.sqrMagnitude);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
        }

        Attack();

        if (rb.velocity.sqrMagnitude > 1)
            rb.velocity.Normalize();        
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            attacked();
        }
    }
}
