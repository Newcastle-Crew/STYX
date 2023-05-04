#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class cannonScript : MonoBehaviour
{
    /// Made with this tutorial: https://youtu.be/SDl5YTis__k
    /// rotations & split shot were all me

    // todo: find a way to show a 'countdown' on screen before the cannon shoots
    // todo: find a way to show the 'cooldown' before the cannon is ready to shoot again

    #region Firepoints
    public Transform firepoint; // The exact area that the cannonball fires from.
    public Transform firepoint2; // additional firepoint, used for split shot
    public Transform firepoint3; // see above
    #endregion
    
    public bool lowerCannon; // Cannons on the bottom of the boat have different rotations.
    public bool splitShot; // upgrade bool, activates the ability to fire two shots diagonally. replaces ability to shoot straight

    public GameObject bullet; // The cannonball that fires.
    public GameObject cannon; // The cannon itself, that can be rotated.

    float timeBetween; // the time between shots (probably will be removed)
    public float startTimeBetween; // begins a countdown between shots

    float cannonZ = 0; // The cannon's Z rotation
    float lowcannonZ = 180; // The cannon's Z rotation
    private bool readyToGo; // Checks for player in the right position (inside trigger box).

    float timeUntilShoot = 5f; // unused; see first TODO

    void Start()
    { timeBetween = startTimeBetween; } // will likely be removed, best for automatic fire

    void Rotate() // rotates the cannon to left, right, or back to middle
    {
        if(!lowerCannon) // if the cannon is on the upper side...
        {
            if (cannonZ + 45 > 45) // and rotating it would put it past the 45 degree mark...
                cannonZ = -45; // put it in the other direction instead
            else
                cannonZ += 45; // otherwise, rotate it no problem

            transform.rotation = Quaternion.Euler(0, 0, cannonZ);
        }

        if(lowerCannon)
        {
            if(lowcannonZ - 45 < 135)
                lowcannonZ = 225;
            else
                lowcannonZ -= 45;

            transform.rotation = Quaternion.Euler(0, 0, lowcannonZ);
        }
    }
    
    void Update()
    {
        if (splitShot == true)
        {
            if (readyToGo == true && Input.GetKeyDown(KeyCode.E) && timeBetween <= 0)
            {
                Instantiate(bullet, firepoint2.position, firepoint2.rotation);
                Instantiate(bullet, firepoint3.position, firepoint3.rotation);
                timeBetween = startTimeBetween;
            }
        }
        if (readyToGo == true && Input.GetKeyDown(KeyCode.E) && timeBetween <= 0)
        {
            Instantiate(bullet,firepoint.position,firepoint.rotation);
            timeBetween = startTimeBetween;
        }
        else
        { 
            timeBetween -= Time.deltaTime;
            // Could add a negative sound effect here to show that it's not ready yet.
        }

        if (readyToGo == true && Input.GetKeyDown(KeyCode.Space) && !splitShot)
        { Rotate(); }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        { readyToGo = true; } // Lets the player fire or rotate the cannon when they're in the right spot.   Bugged currently as enemy weapons are tagged 'player'    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            readyToGo = false; // Stops rotations / firing after leaving the right spot.
        }
    }

    public void SplitShot()
    { splitShot = true; }
}