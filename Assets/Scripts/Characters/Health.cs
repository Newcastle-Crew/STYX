﻿#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

// using these tuts: https://youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM
// also using this tutorial: https://youtu.be/gbFBWxtpgpQ
// and this one: https://youtu.be/v1UGTTeQzbo

public class Health : MonoBehaviour
{
    [SerializeField] public int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    public GameObject cb; // cannonballs
    public GameObject har; // harpoons
    public HealthbarBehaviour healthBar; // enemies' healthbars.
    [SerializeField] public bool isDead = false;
    public GameObject enemy; // spawns enemies

    public void Spawn()
    { gameObject.SetActive(enemy); } // enemies set their own gameObjects to active when it's showtime

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public bool IsAlive() // keeps track of enemies' living status for the wave spawner
    {
        if (currentHealth >= 1) return true;
        else return false;
    }

    private void Update() // updates enemy health bars to show the correct fill and colour
    { healthBar.SetHealth(currentHealth, maxHealth); }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer) // stops player from hurting themselves & enemy friendly fire
            return;

        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, maxHealth);

        if(currentHealth > 0)
        { OnHitWithReference?.Invoke(sender); }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<cannonballScript>() != null)
        { WeBall(); }

        if (collision.gameObject.GetComponent<HarpoonScript>() != null)
        { WePoon(); }
    }

    public void WeBall() // cannonball does 100 damage on hit. Cyoar!
    { GetHit(100, cb); }

    public void WePoon () // harpoon does 2 damage on hit. It's something!
    { GetHit(2, har); }
}