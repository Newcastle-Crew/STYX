# region 'Using' information
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
#endregion

public class WeaponSwitching : MonoBehaviour // this script will be assigned to the player
{
    public GameObject[] weapons; // assign the weapon objects when they have been made
    public PlayerInput pointer; // where the cursor is. makes arrows go where they should

    public GameObject arrowPrefab; // arrows as they're set up in the arrow prefab
    public Transform firepoint; // The exact area that the arrow fires from.
    public WeaponParent wepPar;
    float timeBetween; // time between crossbow shots
    public float startTimeBetween; // begins a countdown between shots

    public int currentWeapon = 0; // starts player with the oar
    private int nrWeapons; // total number of weapons

    [SerializeField] private Image crosshair = null; // Add the crosshair UI element to this via inspector
    private bool isCrosshairActive = false;

    void Start()
    {
        nrWeapons = weapons.Length;
        SwitchWeapon(currentWeapon); // Set default weapons etc
    }

    void Update()
    {
        Vector3 pointerPosition = GameObject.Find("Player").GetComponent<PlayerInput>().GetPointerInput();
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(pointerPosition);
        crosshair.transform.position = screenPosition;

        for (int i = 1; i <= nrWeapons; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                currentWeapon = i - 1;
                SwitchWeapon(currentWeapon);  // Looks complex but basically 'i' is the int and whatever we assign it.
            }

            if (currentWeapon == 1) // if crossbow is equipped
            {
                //wepPar.IsCrossbow = true;

                if (Input.GetKeyDown(KeyCode.Mouse0) && timeBetween <= 0) // and left-click is pressed (while they're able to fire)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && timeBetween <= 0)
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
                else
                {
                    timeBetween -= Time.deltaTime;
                    // TODO: Add a 'negative tone' sound effect here to show that it's not ready yet.
                }
            }
        }

        if (currentWeapon == 1)
        { crosshair.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f); } // Makes the crosshair appear
        else
        { crosshair.color = new Color(126f / 255f, 115f / 255f, 115f / 255f, 100f / 255f); } // Otherwise, makes the crosshair slightly more transparent
    }

    void SwitchWeapon(int index)
    {
        for (int i = 0; i < nrWeapons; i++)
        {
            if (i == index)
            { weapons[i].gameObject.SetActive(true); }
            else
            { weapons[i].gameObject.SetActive(false); }
        }

        if (currentWeapon == 1) // Update crosshair position based on cursor
        {
            //crosshair.color = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f); // Makes the crosshair appear
            Vector3 pointerPosition = GameObject.Find("Player").GetComponent<PlayerInput>().GetPointerInput();
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(pointerPosition);
            crosshair.transform.position = screenPosition;
        }
    }
}