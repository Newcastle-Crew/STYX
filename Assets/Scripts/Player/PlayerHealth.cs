#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
#endregion

// using these tuts: https://youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM
// also using this tutorial: https://youtu.be/gbFBWxtpgpQ

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int currentHealth, maxHealth;
    private bool isDead = false;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public bool IsAlive() // keeps track of enemies' living status for the wave spawner
    {
        if (currentHealth >= 1)
            return true;
        else
            return false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer) // stops player from hurting themselves & enemy friendly fire
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloads the current level after a death
        }
    }
}