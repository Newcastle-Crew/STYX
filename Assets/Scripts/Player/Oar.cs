# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Oar : MonoBehaviour
{
    public float attackDelay = 0.3f; // time between attacks - could be upgradable in future!
    private bool attackBlocked; // stops attack spam
    public bool IsAttacking { get; private set; }
    public Transform circleOrigin; // the centre of the 'circle', where melee attacks come from
    public float attackRadius; // how big the area of the attack is
    public Animator animator; // weapons' animators

    public PlayerWeaponParent wp;
    public GameObject crossbow;

    public void ResetIsAttacking()
    { IsAttacking = false; }

    private void Update()
    {
        if (IsAttacking) return; // prevents attack spam

        if (Input.GetKeyDown(KeyCode.Alpha1)) // pressing 1 on keyboard switches weapons
        {
            wp.SwitchToRanged();
        }
    }

    public void Attack()
    {
        if (attackBlocked || crossbow.activeInHierarchy)
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
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, attackRadius))
        {
            //Debug.Log(collider.name);

            Health health;

            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }
}
