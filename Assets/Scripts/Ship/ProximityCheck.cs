using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityCheck : MonoBehaviour
{
    [SerializeField] List<GameObject> inProximity = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !collision.isTrigger)
        {
            inProximity.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collision.isTrigger)
        {
            inProximity.Remove(collision.gameObject);
        }
    }

    private void Update()
    {
        FindClosestEnemy();
    }

    public GameObject FindClosestEnemy()
    {
        if (inProximity.Count == 0)
            return null;

        float distanceToClosestEnemyAI = Mathf.Infinity;
        GameObject closestEnemyAI = null;
        
        foreach (GameObject go in inProximity)
        {
            float distanceToEnemyAI = (go.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemyAI < distanceToClosestEnemyAI)
            {
                distanceToClosestEnemyAI = distanceToEnemyAI;
                closestEnemyAI = go;
            }
        }

        return closestEnemyAI;
    }
}
