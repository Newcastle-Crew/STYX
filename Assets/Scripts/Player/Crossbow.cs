using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrowPrefab; // arrows as they're set up in the arrow prefab
    public Transform firepoint; // The exact area that the arrow fires from.
    float timeBetween; // time between crossbow shots
    public float startTimeBetween; // begins a countdown between shots. Change in inspector.


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && timeBetween <= 0) // when left-click is pressed while player is able to fire
        {
            ShootArrow();
        }
        timeBetween -= Time.deltaTime;
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firepoint.position, Quaternion.identity);

        // Calculate the direction from the firepoint to the mouse cursor
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z - firepoint.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = targetPosition - firepoint.position;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotate the arrow to point towards the direction of the cursor

        arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * 6; // Set the arrow's velocity to move in the direction of the cursor

        timeBetween = startTimeBetween; // start the cooldown
    }
}
