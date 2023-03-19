#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using these tutorials: https://www.youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer, crossbowRenderer; // gets renderer components
    public Animator animator, crossbowAnimator; // animates weapons

    public Vector2 PointerPosition { get; set; } // checks where the player's mouse is

    public float attackDelay = 0.3f; // time between attacks - could be upgradable in future!
    private bool attackBlocked; // stops attack-spam

    public bool IsAttacking { get; private set; }
    public bool IsCrossbow { get; set; } // fixes a previous issue of left-clicking w/ crossbow out breaking things

    public Transform circleOrigin; // the centre of the 'circle', where the attack comes from
    public float attackRadius; // how big the area of the attack is

    public void ResetIsAttacking()
    { IsAttacking = false; }

    private void Start()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0) scale.x = -1; if (direction.x > 0) scale.x = 1;
        if (direction.x < 0)
        { scale.y = -1; } // inverts the weapon's scale when agent is looking left, making the weapon appear where it should
        else if (direction.x > 0)
        { scale.y = 1; } // doesn't do this when the weapon is past a certain point, cuz that'd make the weapon appear upside-down
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) // player's sprite overlaps their weapon's when it's above their head
        { weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1; }
        else
        { weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1; }

        if (IsCrossbow) // If isCrossbow is true, assign the crossbow renderer and animator to the variables instead
        {
            
            weaponRenderer = crossbowRenderer;
            animator = crossbowAnimator;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, attackRadius); // draws the area of attack circle
    }

    public void DetectColliders() 
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, attackRadius)) // if the weapon's attack close enough to an enemy, this  plays out
        {
            Health health;
            if (health = collider.GetComponent<Health>())
            { health.GetHit(1, transform.parent.gameObject); }
        }
    }
}