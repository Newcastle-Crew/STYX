#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class HarpoonScript : MonoBehaviour
{
    public float speed; // how fast the cannonball moves

    public SpriteRenderer poonSprite; // makes the ball turn invisible after hitting an enemy

    Rigidbody2D rb; // woah, physics!

    public bool Blasting; // supposed to signal the Sound Checks script to play sound, doesn't work rn

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        
        Destroy(gameObject, 3f); // destroys the harpoon 3 seconds after it's been fired
        poonSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            poonSprite.color = new Color(1, 1, 1, 0); // red, blue, green, transparency (1 = visible, 0 = invisible)
            Destroy(gameObject); // destroys the harpoon
        }
    }        
}