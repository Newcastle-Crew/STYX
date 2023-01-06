#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PlayerUpgrades : MonoBehaviour
{
    public AgentMover playerSpeed; // Set via inspector - makes it so only the player gets this upgrade

    private int speedUpgrades = 0; // keeps track of the number of speed upgrades the player has
    public GameObject upgradePoint1; // shows the player how much they've upgraded themselves
    public GameObject upgradePoint2;
    public GameObject upgradePoint3;

    public void UpgradeSpeed()
    {
        if (speedUpgrades < 3) // no more than 3 speed upgrades
        {
            speedUpgrades++; // adds 1 to the upgrade tracker

            if (speedUpgrades >= 3) // if they have 3 upgrades...
            {
                upgradePoint3.SetActive(true); // fill in the last chunk of the bar...
                return; // and stop letting them buy upgrades
            } 
        }

        if (speedUpgrades == 1)
        { upgradePoint1.SetActive(true); }

        if (speedUpgrades >= 2)
        { upgradePoint2.SetActive(true); }

        playerSpeed.upgradeSpeed();
    }
} 
