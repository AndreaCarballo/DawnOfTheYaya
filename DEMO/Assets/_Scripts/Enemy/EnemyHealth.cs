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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        Destroy(gameObject, 2f);

    }



}
