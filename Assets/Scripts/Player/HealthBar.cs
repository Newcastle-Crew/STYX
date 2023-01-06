#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

// Using this tutorial: https://youtu.be/ZzkIn41DFFo

public class HealthBar : MonoBehaviour
{
    public Image healthBar; // set in the inspector - updates the bar
    public Health health; // set in the inspector - updates the player's health value

    float lerpSpeed; // adds a nice delay to the bar moving

    private void Start() // player will always start with a full health bar
    { health.currentHealth = health.maxHealth; }

    private void Update()
    {
        if (health.currentHealth > health.maxHealth) health.currentHealth = health.maxHealth; // ensures health can't go past maximum value
        { lerpSpeed = 3f * Time.deltaTime; } // smoothly animates health bar's movement

        if(health.currentHealth <= 0)
        { health.GetHit(1, gameObject); } // ensures the player actually dies if their health hits 0

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller() 
    { healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)health.currentHealth / (float)health.maxHealth, lerpSpeed); } // makes sure the bar is filled

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (float)health.currentHealth / (float)health.maxHealth);
        healthBar.color = healthColor;
    }

    public void Heal(int healingPoints)
    {
        if (health.currentHealth < health.maxHealth)
            health.currentHealth += healingPoints;
    }
}
