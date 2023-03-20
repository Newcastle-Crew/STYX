#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#endregion

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField] private Transform target; // For most enemies, this is the player. For sculptors, this is the trapdoor.
    [SerializeField] private Transform target2; // For most enemies, this is ignored - for sculptors, this is the player.

    [SerializeField] private float chaseDistanceThreshold = 3, attackDistanceThreshold = 0.8f; // how close player can get before being chased / how close enemies get before attempting a hit

    [SerializeField]
    private float attackDelay = 1; // time between attacks, adjustable
    private float passedTime = 1; // time since last attack, adjustable

    public bool isSculptor = false;

    private void Update()
    {
        if (isSculptor == false && target == null) // if the player is dead, enemies stop moving
        {
            OnMovementInput?.Invoke(Vector2.zero);
            return;
        }
        if(isSculptor == true && target2 == null)
        {
            OnMovementInput?.Invoke(Vector2.zero);
            return;
        }

        if (target == null) // if first target is destroyed, go after secondary target
        {
            float newDistance = Vector2.Distance(target2.position, transform.position);

            if (newDistance < chaseDistanceThreshold) // if the player is close enough to be chased...
            {
                OnPointerInput?.Invoke(target2.position); // look at them

                if (newDistance <= attackDistanceThreshold) // if close enough to hit, do so
                {
                    OnMovementInput?.Invoke(Vector2.zero);

                    if (passedTime >= attackDelay)
                    {
                        passedTime = 0; // wait at least 1 second before hitting
                        OnAttack?.Invoke();
                    }
                }
                else // if not close enough to hit target, chase
                {
                    Vector2 direction = target2.position - transform.position;
                    OnMovementInput?.Invoke(direction.normalized);
                }
            }
            if (passedTime < attackDelay) // even while enemy idles, they're getting ready for attack
            { passedTime += Time.deltaTime; }
        }
        
        if(target != null)
        {
            float distance = Vector2.Distance(target.position, transform.position); // defines the distance between target and enemy
            if (distance < chaseDistanceThreshold) // if the target is close enough to be chased...
            {
                OnPointerInput?.Invoke(target.position); // look at the target

                if (distance <= attackDistanceThreshold) // if close enough to hit, do so
                {
                    OnMovementInput?.Invoke(Vector2.zero); // stop moving when close enough to hit
                    if (passedTime >= attackDelay)
                    {
                        passedTime = 0; // wait at least 1 second before hitting
                        OnAttack?.Invoke(); // hit
                    }
                }
                else // if not close enough to hit target, chase
                {
                    Vector2 direction = target.position - transform.position;
                    OnMovementInput?.Invoke(direction.normalized);
                }
            }
            if (passedTime < attackDelay) // even while enemy idles, they're getting ready for attack
            { passedTime += Time.deltaTime; }
        }
        
    }
}
