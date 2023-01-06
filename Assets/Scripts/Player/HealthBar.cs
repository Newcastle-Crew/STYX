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
    private int upgrades = 0; // keeps track of the number of health upgrades the player has

    public GameObject upgradePoint1; // shows the player how much they've upgraded themselves
    public GameObject upgradePoint2;
    public GameObject upgradePoint3;

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

    public void TestDamage(int damagePoints)
    {
        if (health.currentHealth > 0)
            health.currentHealth -= damagePoints;
    }

    public void UpgradeHealth()
    {
        if (upgrades < 3)
        {
            upgrades++;
            if(upgrades >= 3)
            {
                upgradePoint3.SetActive(true);
                return;
            }
        }

        if (upgrades == 1)
        { upgradePoint1.SetActive(true); }

        if (upgrades >= 2)
        { upgradePoint2.SetActive(true); }

        health.maxHealth += 5;
        health.currentHealth = health.maxHealth;
    }

    public void FullHeal()
    {
        health.currentHealth = health.maxHealth;
    }
}
