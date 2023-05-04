#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#endregion

public class HarpoonGun : MonoBehaviour
{
    [SerializeField] Transform cannonSprite;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject target = null;
    [SerializeField] float angle;
    public bool HasFired { get; private set; }

    public bool lowerPoon; // Cannons on the bottom of the boat have different rotations.

    float rotSpeed = 20; // speed at which the harpoon gun rotates

    void Update() 
    {
        if (enemies.Count != 0) // if there's an enemy in the danger zone
        {
            target = ReturnClosest(); // they (the closest enemy) are the target
            Fire(); // aim the gun at them. basically lets the player know when the harpoon gun is ready to use
        }
        else
            target = null; // or there's no target so the harpoon gun will stay still
    }

    void OnTriggerEnter2D(Collider2D other) // enemy is added to the target list when actively in the danger zone
    {
        if(other.CompareTag("Enemy"))
        { enemies.Add(other.gameObject); }
    }

    void OnTriggerExit2D(Collider2D other) // enemy is removed from target list when not actively in the danger zone
    {
        if (other.CompareTag("Enemy"))
        { enemies.Remove(other.gameObject); }
    }

    GameObject ReturnClosest() // aims the harpoon gun at the closest enemy to itself
    { return enemies.OrderBy(go => (go.transform.position - transform.position).sqrMagnitude).FirstOrDefault(); }

    public void Fire() // doesn't actually 'fire', just points the harpoon gun at the enemy
    {
        if(!HasFired)
            StartCoroutine(RotateToFire(0.75f)); // plays a brief 'rotating' animation before shooting
    }

    IEnumerator RotateToFire(float speed)
    {
        HasFired = true;
        Vector3 targetRot = Vector3.zero;
        Vector3 targetDirection = target.transform.position - transform.position;        
        angle = Vector3.SignedAngle(targetDirection, transform.up, -transform.forward);            

        if(!lowerPoon) // if the harpoon gun is on the upper half...
        {
            if (angle > 45) // and the enemy is beyond 45 degrees...
            { targetRot = new Vector3(0, 0, 45); } // snap the gun to 45 degrees
            else if (angle < -45) // or if the enemy's beyond -45 degrees...
            { targetRot = new Vector3(0, 0, -45); } // snap the gun to -45
            else
            { targetRot = new Vector3(0, 0, angle); } // otherwise, snap it to wherever the enemy is
        }

        if(lowerPoon) // if the harpoon gun is on the lower half...
        {
            if (angle > 225) // and the enemy is beyond 225 degrees...
            { targetRot = new Vector3(0, 0, 225); } // snap the gun to 225 degrees
            //else if (angle > 135) // or if the enemy's beyond 135 degrees...
            //{ targetRot = new Vector3(0, 0, 135); } // snap the gun to negative 135 degrees
            else
            { targetRot = new Vector3(0, 0, angle); } // otherwise, snap it to wherever the enemy is
        }
       
        float alpha = 0;

        while (alpha < speed)
        {
            //cannonSprite.rotation = Quaternion.Euler(Vector3.Lerp(cannonSprite.localEulerAngles, targetRot, alpha / speed));

            cannonSprite.rotation = Quaternion.Slerp(Quaternion.Euler(cannonSprite.localEulerAngles), Quaternion.Euler(targetRot), alpha / speed);

            //cannonSprite.Rotate(targetRot * Time.deltaTime);
            alpha += Time.deltaTime;
            yield return null;
        }

        // print("Fired shot!");

        HasFired = false;
    }
}

