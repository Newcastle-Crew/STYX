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

    [SerializeField] private Transform player;
    [SerializeField] private float chaseDistanceThreshold = 3, attackDistanceThreshold = 0.8f;

    [SerializeField] 
    private float attackDelay = 1;
    private float passedTime = 1;

    private void Update()
    {
        if (player == null) // no point doing anything if player is dead
        {
            OnMovementInput?.Invoke(Vector2.zero);
            return;
        } 

        float distance = Vector2.Distance(player.position, transform.position);
        if(distance < chaseDistanceThreshold) // if the player is close enough to be chased...
        {
            OnPointerInput?.Invoke(player.position); // look at the player

            if(distance <= attackDistanceThreshold) // if close enough to hit the player, do it
            {
                OnMovementInput?.Invoke(Vector2.zero);

                if(passedTime >= attackDelay)
                {
                    passedTime = 0; // wait at least 1 second before hitting player
                    OnAttack?.Invoke();
                }
            }
            else // if not close enough to hit player, chase them
            {
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        if(passedTime < attackDelay) // even while enemy idles, they're getting ready for attack
        {
            passedTime += Time.deltaTime;
        }
    }
}
