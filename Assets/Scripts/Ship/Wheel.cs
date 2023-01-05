#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Wheel : MonoBehaviour
{
    private bool readyToGo; // Checks for player in the right position (inside trigger box).

    // TODO lifted from cannonScript - should ideally be moved into a parent class
    void Update()
    {
        if (readyToGo == true)
        {
            // TODO iterate over to avoid duplicate if statement
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoveShipDown();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveShipUp();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                MoveShipCenter();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { readyToGo = true; } // Lets the player use the wheel when they're in the right spot.
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {  readyToGo = false; } // Stops wheeling after leaving the right spot.
    }

    private void MoveShipUp()
    { Debug.Log("MoveShipUp"); } // TODO: actually move ship up & have a bool for the ship being 'up'

    private void MoveShipDown()
    { Debug.Log("MoveShipDown"); } // TODO: actually move ship down & have a bool for the ship being 'down'

    private void MoveShipCenter()
    { Debug.Log("MoveShipCenter"); } // TODO: actually move ship down & & have a bool for the ship being 'centred'
}