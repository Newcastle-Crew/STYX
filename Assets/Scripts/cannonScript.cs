#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class cannonScript : MonoBehaviour
{

    /// Made with this tutorial: https://youtu.be/SDl5YTis__k
    /// rotations are all me, baby

    // TODO: find a way to make it so the player needs to be near the cannon before it shoots
    // bonus: find a way to show a 'countdown' on screen before the cannon shoots
    // bonus: find a way to show the 'cooldown' before the cannon is ready to shoot again
    
    public Transform firepoint; // The exact area that the cannonball fires from. Middle.

    public Transform cannonRotation; // keeps track of the cannon's 'x' rotation

    public GameObject bullet; // The cannonball that fires.
    public GameObject cannon; // the cannon itself, that can be rotated.

    float timeBetween; // the time between shots (probably will be removed)
    public float startTimeBetween; // begins a countdown between shots

    float timeUntilShoot = 5f; // unused; see first TODO

    float cannonZ = 0; // the cannon's rotation

    private bool readyToGo;

    void Start()
    { timeBetween = startTimeBetween; } // will likely be removed, best for automatic fire

    void Rotate() // rotates the cannon
    {
        if(cannonZ + 45 > 45)
            cannonZ = -45;
        else
            cannonZ += 45;

        transform.rotation = Quaternion.Euler(0, 0, cannonZ);
    }
    
    void Update()
    {
        if(timeBetween <= 0)
        {
            Instantiate(bullet,firepoint.position,firepoint.rotation);
            timeBetween = startTimeBetween;
        }
        else
        { timeBetween -= Time.deltaTime; }

        if(readyToGo == true && Input.GetKeyDown(KeyCode.Space))
        { Rotate(); }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            readyToGo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
    if(other.tag == "Player")
    {
        readyToGo = false;
    }
    }
}
