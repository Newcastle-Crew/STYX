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

    #region Publics 
    public int Level1Obols { get; set; } = 0; // number of obols (coins) the player got in level 1
    public int SpeedUpgrades { get; set; } = 0; // number of speed upgrades the player has unlocked

    public float Acceleration { get; set; } = 50f; // player's acceleration value, tracked for speed upgrades
    public float MaxSpeed { get; set; } = 2f; // ditto above, max speed

    public bool Level1Complete { get; set; } // checks for the completion of the level
    public bool CerberusUnlocked { get; set; } // whether or not the player unlocked cerberus
    public bool BonusCannons { get; set; } // tracks the number of extra cannons the player has unlocked
    public bool BigBalls { get; set; } // tracks the 'Big Balls' cannon upgrade
    public bool SplitShot { get; set; } // tracks the 'Big Balls' cannon upgrade
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

        PlayerData data = new PlayerData(Level1Obols, Level1Complete, CerberusUnlocked, BonusCannons, BigBalls, SplitShot, SpeedUpgrades, Acceleration, MaxSpeed);
        
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
            Level1Complete = data.Level1Complete;
            CerberusUnlocked = data.CerberusUnlocked;
            BonusCannons = data.BonusCannons;
            BigBalls = data.BigBalls;
            SplitShot = data.SplitShot;
            SpeedUpgrades = data.SpeedUpgrades;
            Acceleration = data.Acceleration;
            MaxSpeed = data.MaxSpeed;

            file.Close();
        }
    }

    public void GiveExtraCannons()
    {
        BonusCannons = true;
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
    /// These ints are used in LevelLoader.cs to show the unlocked obols on the level select screen.
    /// </summary>

    private int level1Obols;
    public int Level1Obols => level1Obols;

    #endregion

    #region Completion bools

    /// <summary>
    /// These bool-y bad boys are used in LevelLoader.cs to show the next level on the level select screen when the previous level has been completed.
    /// </summary>

    private bool level1Complete;
    public bool Level1Complete => level1Complete;

    #endregion

    #region Unlockable bools & ints
    /// <summary>
    /// These bad boys are used to make sure anything unlocked, stays that way - between scenes.
    /// </summary>

    private bool cerberusUnlocked;
    public bool CerberusUnlocked => cerberusUnlocked;

    private bool bonusCannons;
    public bool BonusCannons => bonusCannons;

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
    #endregion

    public PlayerData(int l1o, bool l1c, bool dogN, bool cannons, bool balls, bool split, int speed, float acc, float maxspeed)
    {
        level1Obols = l1o;
        level1Complete = l1c;
        cerberusUnlocked = dogN;
        bonusCannons = cannons;
        bigBalls = balls;
        splitShot = split;
        speedUpgrades = speed;
        acceleration = acc;
        maxSpeed = maxspeed;
    }
}