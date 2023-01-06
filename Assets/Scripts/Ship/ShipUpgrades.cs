#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ShipUpgrades : MonoBehaviour
{
    public GameObject bonusCannon1;
    public GameObject bonusCannon2;
    private int bonusCannons; // keeps track of the number of cannons unlocked
    public cannonballScript cannonballs;

    public void BonusCannons()
    {
        if (bonusCannons >= 2) // can only get 2 bonus cannons. should probably disable button after this
            return;

        if (bonusCannons == 0)
        {
            bonusCannon1.SetActive(true);
            bonusCannons = 1;
            return;
        }

        if(bonusCannons == 1)
        {
            bonusCannon2.SetActive(true);
            bonusCannons = 2;
        }
    }

    public void BiggerBalls()
    {
        cannonballs.biggerBalls = true;
    }
}
