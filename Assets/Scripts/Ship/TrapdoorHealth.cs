#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public class TrapdoorHealth : MonoBehaviour
{
    [SerializeField] public int currentHealth, maxHealth;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;

    public Sprite quarterHealth; // Displaying sprite for lowest health.
    public Sprite halfHealth; // Displaying sprite for half health.
    public Sprite threequarterHealth; // Displaying sprite for medium health.
    public Sprite fullHealth; // Displaying sprite for full health.

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public bool IsAlive() // keeps track of trapdoor's health status for the wave spawner
    {
        if (currentHealth >= 1)
            return true;
        else
            return false;
    }

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        TrapdoorHealthCheck();
    }

    private void TrapdoorHealthCheck()
    {
        if(currentHealth == 0)
        {
            // very low obol payout
            Destroy(gameObject);
        }
        if (currentHealth < 5)
        {
            spriteRenderer.sprite = quarterHealth;
            // second lowest obol payout
        }
        else if (currentHealth < 10)
        {
            spriteRenderer.sprite = halfHealth;
            // third lowest obol payout
        }
        else if (currentHealth < 15)
        {
            spriteRenderer.sprite = threequarterHealth;
            // almost peak obol payout
        }
        else
        {
            spriteRenderer.sprite = fullHealth;
            // peak obol payout
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) // prevents the beating of a dead trapdoor
            return;
        if (sender.layer == gameObject.layer) // stops player from hurting themselves & enemy friendly fire
            return;

        if(currentHealth >= 1)
        {
            currentHealth -= amount;
            // sound effect
            // no coin
        }
    }
}