#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

// using these tutorials: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class PlayerWeaponParent : MonoBehaviour
{
    #region Shared components
    public SpriteRenderer characterRenderer, weaponRenderer; // gets the weapons' sprites

    public Vector2 PointerPosition { get; set; } // checks where the player's mouse is
    #endregion

    #region Melee
    public GameObject Melee; // Oar
    #endregion

    #region Ranged
    public GameObject Ranged; // Crossbow
    [SerializeField] private Image crosshair = null; // Add the crosshair UI element to this via inspector
    #endregion

    private void Update()
    {
        Vector3 pointerPosition = GetComponentInParent<PlayerInput>().GetPointerInput(); // pointer position is the cursor's location as used by PlayerInput script
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(pointerPosition); // gets the cursor's position relative to the camera
        crosshair.transform.position = screenPosition; // crosshair follows pointer position     

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0) scale.x = -1; if (direction.x > 0) scale.x = 1;

        if (direction.x < 0)
        { scale.y = -1; } // inverts the weapon's scale when player is looking left, making the weapon appear where it should
        else if (direction.x > 0)
        { scale.y = 1; } // doesn't do this when the weapon is past a certain point, cuz that'd make the weapon appear upside-down
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) // player's sprite overlaps their weapon's when it's above their head
        { weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1; }
        else
        { weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1; }

        if (Ranged.activeInHierarchy)
        { crosshair.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f); } // Makes the crosshair appear fully opaque for crossbow
        else if (Melee.activeInHierarchy)
        { crosshair.color = new Color(126f / 255f, 115f / 255f, 115f / 255f, 100f / 255f); } // Otherwise, makes the crosshair slightly more transparent
    }

    public void SwitchToRanged()
    {
        Melee.SetActive(false);
        Ranged.SetActive(true);
    }

    public void SwitchToMelee()
    {
        Ranged.SetActive(false);
        Melee.SetActive(true);
    }
}