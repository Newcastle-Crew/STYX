#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class ShipUpgrades : MonoBehaviour
{
    private int cannonsUnlocked;
    public GameObject bonusCannon1; // upper row, middle cannon. 
    public GameObject bonusCannon2; // lower row, middle cannon.

    public cannonballScript cannonballs;
    public cannonScript cannonType;

    //private int bonusCannonz; // kept track of the number of cannons unlocked before DataManager took over the job

    private void Start()
    {
        cannonballs.biggerBalls = false;
        cannonType.splitShot = false;

        DataManager.Instance.LoadGame();

        if (DataManager.Instance.BigBalls == true)
        { BiggerBalls(); }

        if (DataManager.Instance.SplitShot == true)
        { SplitShots(); }

        switch (DataManager.Instance.BonusCannons)
        {
            case 1:
                bonusCannon1.SetActive(true);
                break;
            case 2:
                bonusCannon1.SetActive(true);
                bonusCannon2.SetActive(true);
                break;
        }
    }

    public void BonusCannons()
    {
        DataManager.Instance.BonusCannons++;
    }

    public void BiggerBalls()
    { 
        cannonballs.biggerBalls = true;
        cannonType.splitShot = false;
        DataManager.Instance.BigBalls = true;
        DataManager.Instance.EngorgeBalls();
        DataManager.Instance.SaveGame();
    }

    public void SplitShots()
    {
        cannonType.splitShot = true;
        cannonballs.biggerBalls = false;
        DataManager.Instance.SplitShot = true;
        DataManager.Instance.SplitShots();
        DataManager.Instance.SaveGame();
    }
}