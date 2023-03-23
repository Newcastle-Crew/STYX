#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Arrows : MonoBehaviour
{
    public float speed; // how fast the arrows move. set in inspector
    public SpriteRenderer arrowSprite; // makes the arrows turn invisible after hitting an enemy
    public int damage = 3; // how much damage the arrow deals by default
    Rigidbody2D rb; // woah, physics! 

    public bool Firing; /// will do SFX later

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, 1.25f); // destroys the arrow 1.25 seconds after it's been fired
        arrowSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null)
        {
            health.GetHit(damage, transform.gameObject); // arrows do damage as defined above
            /// TODO: add the SFX here
            arrowSprite.color = new Color(1, 1, 1, 0); // red, blue, green, transparency (1 = visible, 0 = invisible)
            Destroy(gameObject); // destroys the arrow
        }
    }
}