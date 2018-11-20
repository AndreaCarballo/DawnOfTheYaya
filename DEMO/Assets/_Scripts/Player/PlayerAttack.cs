using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


    public int Damage = 20;
    public float timebetweenAttacks = 0.5f;
    float timer;
    GameObject enemy;
    EnemyHealth enemyHealth;
    bool enemyinRange;
    bool muerto=false;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyinRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyinRange = false;
        }
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= timebetweenAttacks && enemyinRange )
        {
            Attack();
        }
        if (enemyHealth.currentHealth <= 0)
        {
            if (muerto == false)
            {
            Debug.Log("Enemigo Muerto");
            }
            muerto = true;
            
        }
    }
    void Attack()
    {
        timer = 0f; //reiniciamos contador tiempo 
        if (enemyHealth.currentHealth > 0) //tiene vida el enemigo? 
        {
            Debug.Log("ESTOY VIVO AUN ");
            
            enemyHealth.TakeDamage(Damage); //hacemos daño
        }
    }
}
