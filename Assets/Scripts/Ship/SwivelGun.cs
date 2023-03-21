#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class SwivelGun : MonoBehaviour
{
    float rotSpeed = 5f;
    private Vector3 parentForward;
    private bool hasShot;
    private float fov = 45;
    bool enabled = false;
    bool readyToGo = false;
    public Transform firepoint;
    public GameObject bullet; // The cannonball that fires.
    public GameObject cannon; // The cannon itself, that can be rotated.
    bool hasKilledAI = false;

    public ProximityCheck pCheck;
    private GameObject closestEnemy;

    private void Start()
    {
        pCheck = GetComponentInChildren<ProximityCheck>();
        parentForward = transform.parent.transform.up;
    }

    void Update()
    {
        if (readyToGo)
        { EnableSwivelCannon(); }

        // TODO - more efficient to not run this on every frame, 
        // perhaps only when there are enemies present in the GameObject
        //FindClosestEnemyAI();
        Fire();

        closestEnemy = pCheck.FindClosestEnemy();
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

        // check cannon enabled and that enemy is detected
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

    private void Fire()
    {
        if (closestEnemy == null)
            return;

        Vector3 direction = (closestEnemy.transform.position - parentForward).normalized;

        float angle = Vector3.Angle(transform.up, direction);

        print(angle);

        if (Input.GetKeyDown(KeyCode.Space) && !hasShot)
        {
            Quaternion desired = Quaternion.LookRotation(direction, transform.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, desired, rotSpeed * Time.deltaTime);
            
            //if (angle < (fov * 0.5f))
            //{
            //    //float targetY = transform.localEulerAngles.y + angle;
            //    StartCoroutine(RotateCannon(angle, 0.5f));
            //}
            //else
            //{
            //    StartCoroutine(RotateCannon(45, 0.5f));
            //}

            print("Shot gun");
        }
    }

    private void FireSwivelCannon(Vector3 enemyPosition)

    {
        Vector3 targ = enemyPosition;
        targ.z = 0f;
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        Vector3 direction = (enemyPosition - transform.position).normalized;

        float angleBetween = Vector3.Angle(transform.up, direction);

        Vector3 angle = transform.localEulerAngles;
        //angle.y = Mathf.Clamp(angle.y + Time.deltaTime * rotateRate, -45.0f, 45.0f);
        angle.y = Mathf.Clamp(angle.y, -45.0f, 45.0f);
        transform.localEulerAngles = angle;

        float targetAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        // rotate canon to focus on nearest enemy
        //transform.rotation = Quaternion.Euler(0, 0, 0 + ((angle - 90)));
        transform.Rotate(Vector3.forward, angleBetween); 

        Instantiate(
            bullet,
            firepoint.position,
            // 3rd param to be rotation between firepoint and target
            Quaternion.Euler(new Vector3(0, 0, targetAngle))
        );
        // need to be replaced with a timeout
        hasKilledAI = true;
    }

    IEnumerator RotateCannon(float angle, float speed)
    {
        hasShot = true;
        float alpha = 0;
        Vector3 start = transform.localEulerAngles;
        Vector3 target = new Vector3(start.x, start.y, start.z + angle);

        while(alpha < 1)
        {
            transform.localEulerAngles = Vector3.Lerp(start, target, alpha/speed);
            alpha += Time.deltaTime;
            yield return null;
        }

        hasShot = false;
    }
}
