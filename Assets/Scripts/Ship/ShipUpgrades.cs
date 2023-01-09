#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class ShipUpgrades : MonoBehaviour
{
    public GameObject bonusCannon1; // upper row, middle cannon. 
    public GameObject bonusCannon2; // lower row, middle cannon.

    public GameObject ballIcon; // icon showing that the cannons have the big balls upgrade
    public GameObject splitIcon; // icon showing that the cannons have the split shot upgrade

    public cannonballScript cannonballs;
    public cannonScript cannonType;

    //private int bonusCannonz; // kept track of the number of cannons unlocked before DataManager took over the job

    private void Start()
    {
        cannonballs.biggerBalls = false;
        cannonType.splitShot = false;

        DataManager.Instance.LoadGame();

        if (DataManager.Instance.BonusCannons == true) // only adds the bonus cannons to the boat if the DataManager says so
        { BonusCannons(); }

        if(DataManager.Instance.BigBalls == true)
        { BiggerBalls(); }

        if (DataManager.Instance.SplitShot == true)
        { SplitShots(); }
    }

    public void BonusCannons()
    {
        bonusCannon1.SetActive(true);
        bonusCannon2.SetActive(true);
        DataManager.Instance.BonusCannons = true; // keeps this 'true'-ness true between scenes
        DataManager.Instance.SaveGame(); // saves the game
    }

    public void BiggerBalls()
    { 
        cannonballs.biggerBalls = true;
        cannonType.splitShot = false;
        ballIcon.SetActive(true);
        splitIcon.SetActive(false);
        DataManager.Instance.BigBalls = true;
        DataManager.Instance.EngorgeBalls();
        DataManager.Instance.SaveGame();
    }

    public void SplitShots()
    {
        cannonType.splitShot = true;
        cannonballs.biggerBalls = false;
        splitIcon.SetActive(true);
        ballIcon.SetActive(false);
        DataManager.Instance.SplitShot = true;
        DataManager.Instance.SplitShots();
        DataManager.Instance.SaveGame();
    }
}