#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class SwivelGun : MonoBehaviour
{
    bool enabled = false;
    bool readyToGo = false;
    public Transform firepoint;
    public GameObject bullet; // The cannonball that fires.
    public GameObject cannon; // The cannon itself, that can be rotated.
    bool hasKilledAI = false;

    void Update()
    {
        if (readyToGo)
        { EnableSwivelCannon(); }

        // TODO - more efficient to not run this on every frame, 
        // perhaps only when there are enemies present in the GameObject
        FindClosestEnemyAI();
    }
    
    void FindClosestEnemyAI() // Determines location of closest enemy
    {
        float distanceToClosestEnemyAI = Mathf.Infinity;
        EnemyAI closestEnemyAI = null;
        EnemyAI[] allEnemies = GameObject.FindObjectsOfType<EnemyAI>();

        foreach (EnemyAI currentEnemyAI in allEnemies)
        {
            float distanceToEnemyAI = (currentEnemyAI.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemyAI < distanceToClosestEnemyAI)
            {
                distanceToClosestEnemyAI = distanceToEnemyAI;
                closestEnemyAI = currentEnemyAI;
            }
        }

        // check canon enabled and that enemy is detected
        // has temp block instead of cooldown
        if (enabled && closestEnemyAI && !hasKilledAI)
        {
            Debug.Log(closestEnemyAI);
            // To see in Unity, must enable Gizmos
            Debug.DrawLine(this.transform.position, closestEnemyAI.transform.position);

            // fire canon at ai
            FireSwivelCannon(closestEnemyAI.transform.position);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            readyToGo = true; // Lets the player fire or rotate the cannon when they're in the right spot.
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            readyToGo = false; // Stops rotations / firing after leaving the right spot.
        }
    }

    private void EnableSwivelCannon()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            enabled = false;
            hasKilledAI = false;
        }
    }

    private void FireSwivelCannon(Vector3 enemyPosition)

    {
        Vector3 targ = enemyPosition;
        targ.z = 0f;
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        // rotate canon to focus on nearest enemy
        transform.rotation = Quaternion.Euler(0, 0, 0 + ((angle - 90)));

        Instantiate(
            bullet,
            firepoint.position,
            // 3rd param to be rotation between firepoint and target
            Quaternion.Euler(new Vector3(0, 0, angle))
        );
        // need to be replaced with a timeout
        hasKilledAI = true;
    }
}
