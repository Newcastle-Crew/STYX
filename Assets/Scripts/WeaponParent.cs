﻿#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using these tutorials: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer; // gets the weapon's sprite
    public Animator animator; // animates the weapon

    public Vector2 PointerPosition { get; set; } // checks where the player's mouse is

    public float attackDelay = 0.3f; // time between attacks - could be upgradable in future!
    private bool attackBlocked; // stops attack-spam dead

    public bool IsAttacking { get; private set; }

    public void ResetIsAttacking()
    { IsAttacking = false; }    

    private void Update()
    {
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        { scale.y = -1; } // inverts the weapon's scale when the player is looking 'behind' themselves, making the weapon appear 'behind' them too
        else if (direction.x > 0)
        { scale.y = 1; } // doesn't do this when the weapon is past a certain point, cuz that'd make the weapon appear upside-down
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) // player's sprite overlaps their weapon's when it's above their head
        { weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1; }
        else
        {  weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1; }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        IsAttacking = true;
        StartCoroutine(DelayAttack()); // adds the delay after a successful attack
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }
}
