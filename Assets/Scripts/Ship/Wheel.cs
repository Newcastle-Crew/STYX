#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Wheel : MonoBehaviour
{
    #region Irrelevants
    private bool readyToGo; // Checks for player in the right position (inside trigger box).
    Animator anim; // wheel's animator
    public Animator shipAnim; // attached to the moveThis game object. ship's animator
    public BoxCollider2D mover; // the wheel's IsTrigger boxcollider that lets players move the boat
    public GameObject battleStarter; // the battle starting box. don't start the fight til the boat is middled!

    public enum BoatState
    {
        Down,
        Mid,
        Up
    }

    private BoatState currentBoatState;

    public GameObject dock; // dock that player uses to exit level
    public GameObject moveThis; // physical object that the boat n other stuff is attached to for moving purposes
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>(); // wheel animator. make sure it's attached to the actual wheel
        currentBoatState = BoatState.Down; // boat starts near the dock
    }

    void Update()
    {
        if (readyToGo == true) // if player is in the 'interact box'
        {
            if (Input.GetKeyDown(KeyCode.E)) // and presses 'E'
            {
                switch (currentBoatState)
                {
                    case BoatState.Down:
                        MoveShipMiddle();
                        break;
                    case BoatState.Mid:
                        MoveShipUp();
                        break;
                    case BoatState.Up:
                        MoveShipDown();
                        break;
                }

                battleStarter.SetActive(true);
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
    {
        currentBoatState = BoatState.Up;
        anim.Play("WheelMove");
        shipAnim.Play("ShipMovingUp");
        moveThis.transform.position = new Vector3(-3.6f, -4.087925f, 0);
    } 

    private void MoveShipDown()
    {
        if (currentBoatState == BoatState.Up)
        {
            shipAnim.Play("ShipWacky");
        }
        else if (currentBoatState == BoatState.Mid)  // this one should only play if the player is in the middle when the level ends
        {
            shipAnim.Play("ShipMovingDown");
        }
        else if (currentBoatState == BoatState.Down)
            return;

        currentBoatState = BoatState.Down;
        anim.Play("WheelMove");

        moveThis.transform.position = new Vector3(-3.6f, -4.087925f, 0);
    } 

    private void MoveShipMiddle()
    {
        if (currentBoatState == BoatState.Up)
        {
            shipAnim.Play("MovingShipMid2");
        }
        else if (currentBoatState == BoatState.Down)
        {
            shipAnim.Play("MovingShipMid1");
        }

        currentBoatState = BoatState.Mid;
        anim.Play("WheelMove");

        moveThis.transform.position = new Vector3(-3.6f, -5.58f, 0);
    }

    public void LevelFinished()
    {
        MoveShipDown(); // move ship down, to where the dock is
        mover.enabled = false; // no more ship moving
        dock.SetActive(true); // dock appears
    }
}