#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class cannonScript : MonoBehaviour
{
    /// Made with this tutorial: https://youtu.be/SDl5YTis__k
    /// rotations were all me

    // todo: find a way to show a 'countdown' on screen before the cannon shoots
    // todo: find a way to show the 'cooldown' before the cannon is ready to shoot again
    
    public Transform firepoint; // The exact area that the cannonball fires from. More could be added for upgrades or something.

    public GameObject bullet; // The cannonball that fires.
    public GameObject cannon; // The cannon itself, that can be rotated.

    float timeBetween; // the time between shots (probably will be removed)
    public float startTimeBetween; // begins a countdown between shots

    float cannonZ = 0; // The cannon's Z rotation
    private bool readyToGo; // Checks for player in the right position (inside trigger box).

    float timeUntilShoot = 5f; // unused; see first TODO

    void Start()
    { timeBetween = startTimeBetween; } // will likely be removed, best for automatic fire

    void Rotate() // rotates the cannon to left, right, or back to middle
    {
        if(cannonZ + 45 > 45)
            cannonZ = -45;
        else
            cannonZ += 45;

        transform.rotation = Quaternion.Euler(0, 0, cannonZ);
    }
    
    void Update()
    {
        if(readyToGo == true && Input.GetKeyDown(KeyCode.E) && timeBetween <= 0)
        {
            Instantiate(bullet,firepoint.position,firepoint.rotation);
            timeBetween = startTimeBetween;
        }
        else
        { 
            timeBetween -= Time.deltaTime;
            // Could add a negative sound effect here to show that it's not ready yet.
        }

        if(readyToGo == true && Input.GetKeyDown(KeyCode.Space))
        { Rotate(); }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            readyToGo = true; // Lets the player fire or rotate the cannon when they're in the right spot.
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
    if(other.tag == "Player")
    {
        readyToGo = false; // Stops rotations / firing after leaving the right spot.
        }
    }
}
