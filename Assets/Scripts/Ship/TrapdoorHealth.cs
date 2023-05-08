#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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

    [SerializeField] TMP_Text ObolText;

    public static int totalCoins; // use this for upgrades n stuff
    public int thisLevelCoins; // find a way to add this to 

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
        thisLevelCoins = 15;
    }

    public bool IsAlive() // keeps track of trapdoor's health status for the wave spawner
    {
        if (currentHealth >= 1)
            return true;
        else
            return false;
    }

    private void Start()
    { spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); }

    private void FixedUpdate()
    {
        TrapdoorHealthCheck();
    }

    private void TrapdoorHealthCheck()
    {
        if(currentHealth == 0)
        {
            thisLevelCoins = 3;
            ObolText.text = "3";
            Destroy(gameObject);
        }
        if (currentHealth < 5)
        {
            spriteRenderer.sprite = quarterHealth;
            thisLevelCoins = 6;
            ObolText.text = "3";
        }
        else if (currentHealth < 10)
        {
            spriteRenderer.sprite = halfHealth;
            thisLevelCoins = 9;
            ObolText.text = "9";
        }
        else if (currentHealth < 15)
        {
            spriteRenderer.sprite = threequarterHealth;
            thisLevelCoins = 12;
            ObolText.text = "12";
        }
        else
        {
            spriteRenderer.sprite = fullHealth;
            thisLevelCoins = 15;
            ObolText.text = "15";
        }
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead) // prevents the beating of a dead trapdoor
            return;
        if (sender.layer == gameObject.layer) // stops friendly fire
            return;

        if(currentHealth >= 1) // every time the trapdoor takes a hit
        {
            currentHealth -= amount;
            // sound effect here
        }
    }

    public void EndLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "GameScene":
                if (DataManager.Instance.LevelsComplete < 1) // if the player has never beaten a level
                { DataManager.Instance.LevelsComplete = 1; } // then they've now beaten level 1
                DataManager.Instance.SaveGame(); // saves the game
                break;
            case "Level2":
                if (DataManager.Instance.LevelsComplete < 2)
                { DataManager.Instance.LevelsComplete = 2; }
                DataManager.Instance.SaveGame();
                break;
            //case "Level3":
            //    if (DataManager.Instance.LevelsComplete < 3)
            //    { DataManager.Instance.LevelsComplete = 3; }
            //    DataManager.Instance.SaveGame();
            //    break;
            //case "Level4":
            //    if (DataManager.Instance.LevelsComplete < 4)
            //    { DataManager.Instance.LevelsComplete = 4; }
            //    DataManager.Instance.SaveGame();
            //    break;
            //case "Level5":
            //    if (DataManager.Instance.LevelsComplete < 5)
            //    { DataManager.Instance.LevelsComplete = 5; }
            //    DataManager.Instance.SaveGame();
            //    break;
        }

        DataManager.Instance.SaveGame();
        thisLevelCoins += totalCoins;
    }
}