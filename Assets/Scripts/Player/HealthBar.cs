#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

// Using this tutorial: https://youtu.be/ZzkIn41DFFo

public class HealthBar : MonoBehaviour
{
    public Health health; // set in the inspector - updates the player's health value, and ONLY the player's health value

    #region UI stuff
    private int healthUpgrades; // keeps track of the number of health upgrades the player has
    public GameObject upgradePoint1; // shows the player how much they've upgraded themselves
    public GameObject upgradePoint2; // and are directly tied to healthUpgrades
    public GameObject upgradePoint3;
    public Image healthBar; // set in the inspector - updates the bar
    #endregion

    float lerpSpeed; // adds a nice delay to the bar moving

    private void Start() // player will always start with a full health bar
    {
        DataManager.Instance.LoadGame();
        healthUpgrades = DataManager.Instance.HealthUpgrades;

        UpdatePlayerHealth();

        health.currentHealth = DataManager.Instance.MaxHealth; // formerly currentHealth = health.maxHealth

        switch (healthUpgrades) // shows the bars after leaving & re-entering the scene
        {
            case 1:
                upgradePoint1.SetActive(true);
                break;
            case 2:
                upgradePoint1.SetActive(true);
                upgradePoint2.SetActive(true);
                break;
            case 3:
                upgradePoint1.SetActive(true);
                upgradePoint2.SetActive(true);
                upgradePoint3.SetActive(true);
                break;
        }
    }

    public void UpgradeHealth()
    {
        if (healthUpgrades < 3) // no more than 3 health upgrades
        {
            healthUpgrades++; // adds 1 to the upgrade tracker
            DataManager.Instance.HealthUpgrades = healthUpgrades; // keeps the DataManager updated  
            DataManager.Instance.MaxHealth += 5; // upgrades MaxHealth in a stays-between-scenes way as it's upgraded here, too   

            switch (healthUpgrades) // shows the player how much they've upgraded themselves
            {
                case 1:
                    upgradePoint1.SetActive(true);
                    break;
                case 2:
                    upgradePoint2.SetActive(true);
                    break;
                case 3:
                    upgradePoint3.SetActive(true);
                    break;
            }
            health.maxHealth += 5;  
            health.currentHealth = DataManager.Instance.MaxHealth; // formerly health.currentHealth = health.maxHealth;
            DataManager.Instance.HealthUpgrades = healthUpgrades;
            Debug.Log("Health has been upgraded: " + DataManager.Instance.HealthUpgrades);
        }
        DataManager.Instance.SaveGame();
    }

    private void Update()
    {
        if (health.currentHealth > DataManager.Instance.MaxHealth) health.currentHealth = DataManager.Instance.MaxHealth; // ensures health can't go past maximum value // formerly if (health.currentHealth > health.maxHealth) health.currentHealth = health.maxHealth;
        { lerpSpeed = 3f * Time.deltaTime; } // smoothly animates health bar's movement

        if(health.currentHealth <= 0)
        { health.GetHit(1, gameObject); } // ensures the player actually dies if their health hits 0

        HealthBarFiller(); // keeps the health bar filled, every frame
        ColorChanger(); // keeps the health bar changing colours, every frame
    }

    void HealthBarFiller() // makes sure the bar is filled
    { healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)health.currentHealth / (float)DataManager.Instance.MaxHealth, lerpSpeed); } // formerly healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)health.currentHealth / (float)health.maxHealth, lerpSpeed);

    void ColorChanger()
    {
        if(healthUpgrades == 0)
        { Color healthColor = Color.Lerp(Color.red, Color.yellow, (float)health.currentHealth / (float)DataManager.Instance.MaxHealth); healthBar.color = healthColor; }
        if(healthUpgrades == 1)
        { Color healthColor = Color.Lerp(Color.red, Color.cyan, (float)health.currentHealth / (float)DataManager.Instance.MaxHealth); healthBar.color = healthColor; }    
        if(healthUpgrades == 2)
        { Color healthColor = Color.Lerp(Color.red, Color.green, (float)health.currentHealth / (float)DataManager.Instance.MaxHealth); healthBar.color = healthColor; }
        if (healthUpgrades == 3)
        { Color healthColor = Color.Lerp(Color.red, Color.magenta, (float)health.currentHealth / (float)DataManager.Instance.MaxHealth); healthBar.color = healthColor; }
    }

    public void TestHeal(int healingPoints)
    {
        if (health.currentHealth < DataManager.Instance.MaxHealth)
            health.currentHealth += healingPoints;
    }

    public void TestDamage(int damagePoints)
    {
        if (health.currentHealth > 0)
            health.currentHealth -= damagePoints;
    }

    public void FullHeal()
    { health.currentHealth = DataManager.Instance.MaxHealth; }

    public void UpdatePlayerHealth()
    {
        healthUpgrades = DataManager.Instance.HealthUpgrades;
        health.maxHealth = DataManager.Instance.MaxHealth;
    }
}