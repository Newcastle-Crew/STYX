#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class cannonballScript : MonoBehaviour
{
    public float speed; // how fast the cannonball moves
    public SpriteRenderer ballSprite; // used to make the ball turn invisible after hitting an enemy

    Rigidbody2D rb;
    int enemyHealth = 2;

    public bool Blasting; // supposed to signal the Sound Checks script to play sound, doesn't work

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        
        Destroy(gameObject, 2f); // destroys the cannonball 2 seconds after it's been fired

        ballSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<simpleEnemy>() != null)
        {   
            Destroy(collision.gameObject); // destroys the collisions of the enemy

            ballSprite.color = new Color(1, 1, 1, 0); // red, blue, green, transparency (1 = visible, 0 = invisible)

            Destroy(gameObject); // destroys the cannonball
        }
    }        
}