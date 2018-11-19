using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startHealth = 100;
    public int currentHealth;
    bool isDead;
    CapsuleCollider capsuleCollider;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        isDead = true;
        Destroy(this.gameObject, 0f);
    }



}
