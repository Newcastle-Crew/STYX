#region 'Using' info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class HarpoonFire : MonoBehaviour
{
    [SerializeField] HarpoonGun cannon;
    public Transform firepoint;
    public GameObject bullet; // The cannonball that fires.
    float timeBetween; // the time between shots (probably will be removed)
    public float startTimeBetween; // begins a countdown between shots

    void Start()
    { timeBetween = startTimeBetween; } // will likely be removed, best for automatic fire

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E) ) // && !cannon.HasFired
            { 
                Instantiate(bullet,firepoint.position,firepoint.rotation);
                timeBetween = startTimeBetween;
                cannon.Fire();
            }
        }
    }
}