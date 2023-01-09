#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class cannonballScript : MonoBehaviour
{
    public float speed; // how fast the cannonball moves

    public SpriteRenderer ballSprite; // makes the ball turn invisible after hitting an enemy

    public bool biggerBalls;
    public float scaleFactor = 2f;

    Rigidbody2D rb; // woah, physics!

    public bool Blasting; // supposed to signal the Sound Checks script to play sound, doesn't work rn

    void Start()
    {
        if (biggerBalls)
        { transform.localScale *= scaleFactor; }

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        
        Destroy(gameObject, 1.5f); // destroys the cannonball 1.5 seconds after it's been fired
        ballSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            ballSprite.color = new Color(1, 1, 1, 0); // red, blue, green, transparency (1 = visible, 0 = invisible)
            Destroy(gameObject); // destroys the cannonball
        }
    }        
}