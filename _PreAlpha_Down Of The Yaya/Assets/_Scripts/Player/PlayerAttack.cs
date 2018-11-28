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
    private Animator animPlayer;

    private void Awake()
    {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            animPlayer = GetComponent<Animator>();

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

        if (enemy != null)
        {
            timer += Time.deltaTime;
            if (timer >= timebetweenAttacks && enemyinRange)
            {
                Attack();
            }
            if (enemyHealth.currentHealth <= 0)
            {
                //Debug.Log("Enemigo Muerto");
                enemyHealth.Death();
            }
        }
        
    }
    void Attack()
    {
        timer = 0f; //reiniciamos contador tiempo 
        if (enemyHealth.currentHealth > 0) //tiene vida el enemigo? 
        {
            animPlayer.SetTrigger("AttackHuman");
            enemyHealth.TakeDamage(Damage); //hacemos daño
        }
    }
}
