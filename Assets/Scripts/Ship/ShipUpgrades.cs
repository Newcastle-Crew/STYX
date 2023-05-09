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

    public GameObject splitShotIndicator;
    public GameObject bigBallIndicator;

    public cannonballScript cannonballs;
    public cannonScript cannonType;

    //private int bonusCannonz; // kept track of the number of cannons unlocked before DataManager took over the job

    private void Start()
    {
        cannonballs.biggerBalls = false;
        cannonType.splitShot = false;

        DataManager.Instance.LoadGame();

        if (DataManager.Instance.BigBalls == true)
        { 
            BiggerBalls();
            bigBallIndicator.SetActive(true);
        }

        if (DataManager.Instance.SplitShot == true)
        { 
            SplitShots();
            splitShotIndicator.SetActive(true);
        }

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

    public void BonusCannons() // bonus cannons cost 10 obols
    {
        if(DataManager.Instance.TotalObols >= 10)
        {
            if (DataManager.Instance.BonusCannons == 0)
            {
                DataManager.Instance.BonusCannons = 1;
                bonusCannon1.SetActive(true);
                DataManager.Instance.TotalObols -= 10;
            }
            else if (DataManager.Instance.BonusCannons == 1)
            {
                DataManager.Instance.BonusCannons = 2;
                bonusCannon1.SetActive(true);
                bonusCannon2.SetActive(true);
                DataManager.Instance.TotalObols -= 10;
            }
        }
    }

    public void BiggerBalls()
    {   
        if(DataManager.Instance.TotalObols >= 10)
        {
            cannonballs.biggerBalls = true;
            cannonType.splitShot = false;
            DataManager.Instance.BigBalls = true;
            bigBallIndicator.SetActive(true);
            splitShotIndicator.SetActive(false);
            DataManager.Instance.EngorgeBalls();
            DataManager.Instance.SaveGame();
        }
    }

    public void SplitShots()
    {
        if (DataManager.Instance.TotalObols >= 10)
        {
            cannonType.splitShot = true;
            cannonballs.biggerBalls = false;
            bigBallIndicator.SetActive(false);
            splitShotIndicator.SetActive(true);
            DataManager.Instance.SplitShot = true;
            DataManager.Instance.SplitShots();
            DataManager.Instance.SaveGame();
        }
    }
}