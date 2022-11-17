using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        PlayerMovement.attacked += EnableWeapon;

        boxCollider = GetComponent<BoxCollider>();

        boxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
            print("Hit enemy");
    }

    void EnableWeapon()
    {
        boxCollider.enabled = true;
        Invoke("ResetTrigger", 0.5f);
    }

    void ResetTrigger()
    {
        boxCollider.enabled = false;
    }
}
