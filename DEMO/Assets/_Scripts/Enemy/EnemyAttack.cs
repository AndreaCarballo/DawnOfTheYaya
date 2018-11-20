using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {


    public float timebetweenAttacks = 0.5f; // tiempo entre ataques
    public int attackDamage = 10; //daño de ataque
    GameObject player;
    PlayerHealth playerHealth;
    bool playerinrange;// el jugador esta al lado??
    float timer; //timepo que pasa desde el ultimo ataque
    EnemyHealth enemyHealth;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

   
	
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject== player)
        {
            playerinrange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject== player)
        {
            playerinrange = false;
        }
    }
    
    
    // Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>= timebetweenAttacks && playerinrange)// && enemyHealth.currentHealth>0)
        {
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            Debug.Log("JUgador DEADDDD");
        }
	}

    void Attack()
    {
        timer = 0f; //reiniciamos contador tiempo 
        if (playerHealth.currentHealth > 0) //tiene vida el jugador? 
        {
            playerHealth.TakeDamage(attackDamage); //hacemos daño
        }
    }



}
