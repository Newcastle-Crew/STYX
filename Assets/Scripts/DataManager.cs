#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
#endregion

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    #region Privates

    int levelsComplete;
    int totalObols;

    #endregion

    #region Publics 

    public int TotalObols { get; set; } = 0; // number of obols the player has. used to buy upgrades
    public int SpeedUpgrades { get; set; } = 0; // number of speed upgrades the player has unlocked
    public int HealthUpgrades { get; set; } = 0; // number of health upgrades the player has unlocked
    public int MaxHealth { get; set; } = 10; // amount of health the player has
    public int LevelsComplete { get; set; } = 0; // tracks the number of levels completed for level select screen and obol tracking purposes
    public int Level1Obols { get; set; } = 0; // number of obols the player got in level 1
    public int Level2Obols { get; set; } = 0; // number of obols the player got in level 2
    public int Level3Obols { get; set; } = 0; // number of obols the player got in level 3
    public int Level4Obols { get; set; } = 0; // number of obols the player got in level 4
    public int Level5Obols { get; set; } = 0; // number of obols the player got in level 5

    public float Acceleration { get; set; } = 50f; // player's acceleration value, tracked for speed upgrades
    public float MaxSpeed { get; set; } = 2f; // ditto above, max speed

    public bool CerberusUnlocked { get; set; } // whether or not the player unlocked cerberus
    public int BonusCannons { get; set; } = 0; // tracks the number of extra cannons the player has unlocked
    public bool BigBalls { get; set; } // tracks the 'Big Balls' cannon upgrade
    public bool SplitShot { get; set; } // tracks the 'Split Shot' cannon upgrade

    #endregion

    public void Awake()
    {
        if (Instance != null ) // if an instance of the datamanager already exists, and it's not supposed to
        { Destroy(gameObject); } // destroy it
        else
        { Instance = this; } // and make this one the real slim shady. makes sure the thing works, and stops the game from making more when a new scene's loaded.

        DontDestroyOnLoad(gameObject);
        LoadGame();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");

        PlayerData data = new PlayerData(Level1Obols, Level2Obols, Level3Obols, Level4Obols, Level5Obols, TotalObols,
                                            LevelsComplete, CerberusUnlocked, BonusCannons, BigBalls, SplitShot, SpeedUpgrades,
                                            Acceleration, MaxSpeed, HealthUpgrades, MaxHealth);
        
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);

            Level1Obols = data.Level1Obols;
            Level2Obols = data.Level2Obols;
            Level3Obols = data.Level3Obols;
            Level4Obols = data.Level4Obols;
            Level5Obols = data.Level5Obols;
            TotalObols = data.TotalObols;
            LevelsComplete = data.LevelsComplete;
            CerberusUnlocked = data.CerberusUnlocked;
            BonusCannons = data.BonusCannons;
            BigBalls = data.BigBalls;
            SplitShot = data.SplitShot;
            SpeedUpgrades = data.SpeedUpgrades;
            Acceleration = data.Acceleration;
            MaxSpeed = data.MaxSpeed;
            HealthUpgrades = data.HealthUpgrades;
            MaxHealth = data.MaxHealth;
            file.Close();
        }
    }

    public void GiveExtraCannons()
    {
        BonusCannons++;
        SaveGame();
    }

    public void EngorgeBalls()
    {
        BigBalls = true;
        SplitShot = false;
        SaveGame();
    }

    public void SplitShots()
    {
        SplitShot = true;
        BigBalls = false;
        SaveGame();
    }
}

[Serializable]
public class PlayerData
{
    #region Obol counters

    /// <summary>
    /// These ints are used in LevelLoader.cs to show the unlocked obols on the level select screen, and prevent spamming level 1 to get all upgrades.
    /// </summary>

    private int level1Obols;
    public int Level1Obols => level1Obols;

    private int level2Obols;
    public int Level2Obols => level2Obols;

    private int level3Obols;
    public int Level3Obols => level3Obols;

    private int level4Obols;
    public int Level4Obols => level4Obols;

    private int level5Obols;
    public int Level5Obols => level5Obols;

    private int totalObols;
    public int TotalObols => totalObols;

    #endregion

    #region Completion bools

    /// <summary>
    /// These ints are used in LevelInfo.cs to show the next level on the level select screen when the previous level has been completed.
    /// </summary>

    private int levelsComplete;
    public int LevelsComplete => levelsComplete;

    #endregion

    #region Unlockable bools & ints
    /// <summary>
    /// These bad boys are used to make sure anything unlocked, stays that way - between scenes.
    /// </summary>

    private bool cerberusUnlocked;
    public bool CerberusUnlocked => cerberusUnlocked;

    private int bonusCannons;
    public int BonusCannons => bonusCannons;

    private bool bigBalls;
    public bool BigBalls => bigBalls;

    private bool splitShot;
    public bool SplitShot => splitShot;

    private int speedUpgrades;
    public int SpeedUpgrades => speedUpgrades;

    private float acceleration;
    public float Acceleration => acceleration;

    private float maxSpeed;
    public float MaxSpeed => maxSpeed;

    private int healthUpgrades;
    public int HealthUpgrades => healthUpgrades;

    private int maxHealth;
    public int MaxHealth => maxHealth;
    #endregion

    public PlayerData(int l1o, int l2o, int l3o, int l4o, int l5o, int to, int lc, bool dogN, int cannons, bool balls, bool split, int speed, float acc, float maxspeed, int uhealth, int mhealth)
    {
        level1Obols = l1o;
        level2Obols = l2o;
        level3Obols = l3o;
        level4Obols = l4o;
        level5Obols = l5o;
        totalObols = to;
        levelsComplete = lc;
        cerberusUnlocked = dogN;
        bonusCannons = cannons;
        bigBalls = balls;
        splitShot = split;
        speedUpgrades = speed;
        acceleration = acc;
        maxSpeed = maxspeed;
        healthUpgrades = uhealth;
        maxHealth = mhealth;
    }
}