#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PlayerUpgrades : MonoBehaviour
{
    public PlayerAgentMover playerSpeed; // Set via inspector - makes it so only the player gets this upgrade

    #region UI stuff
    private int speedUpgrades = 0; // keeps track of the number of speed upgrades the player has
    public GameObject upgradePoint1; // shows the player how much they've upgraded themselves
    public GameObject upgradePoint2; // and are directly tied to speedUpgrades
    public GameObject upgradePoint3;
    public GameObject CerbUI; // the UI button for summoning Cerberus
    #endregion

    private void Start()
    {
        //playerSpeed.acceleration = 50f;
        //playerSpeed.maxSpeed = 2f;
        DataManager.Instance.LoadGame();

        speedUpgrades = DataManager.Instance.SpeedUpgrades;

        UpdatePlayerSpeed();

        if (DataManager.Instance.CerberusUnlocked == true)
        { CerbUI.SetActive(true); }

        playerSpeed.acceleration = DataManager.Instance.Acceleration;
        playerSpeed.maxSpeed = DataManager.Instance.MaxSpeed;      

        switch(speedUpgrades) // shows the bars after leaving & re-entering the scene
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

    public void UpgradeSpeed()
    {
        if (speedUpgrades < 3) // no more than 3 speed upgrades
        {
            speedUpgrades++; // adds 1 to the upgrade tracker
            DataManager.Instance.SpeedUpgrades = speedUpgrades; // keeps the DataManager updated
            DataManager.Instance.Acceleration += 2.5f; // upgrades Acceleration in a stays-between-scenes way as it's upgraded here, too
            DataManager.Instance.MaxSpeed += 1f; // upgrades Acceleration in a stays-between-scenes way as it's upgraded here, too   

            switch (speedUpgrades) // shows the player how much they've upgraded themselves
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
            playerSpeed.upgradeSpeed();
        }

        DataManager.Instance.SaveGame();
    }

    public void UpdatePlayerSpeed()
    {
        playerSpeed.acceleration = DataManager.Instance.Acceleration;
        playerSpeed.maxSpeed = DataManager.Instance.MaxSpeed;
    }
} 
