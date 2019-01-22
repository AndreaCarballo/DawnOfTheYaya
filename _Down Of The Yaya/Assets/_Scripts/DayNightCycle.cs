using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DayNightCycle : MonoBehaviour
{

    public float velocity;
    public AudioSource nightSource;
    public AudioSource daySource;
    public bool playSound;
    private GameObject[] enemies;
    private GameObject playerObject;
    private float inicialSpeed;


    // Use this for initialization
    void Start()
    {
        velocity = 0.75f;
        playSound = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        inicialSpeed = enemies[0].GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0.05 && transform.position.y >= -0.05)
            playSound = true;

        transform.RotateAround(Vector3.zero, Vector3.right, velocity * Time.deltaTime);
        transform.LookAt(Vector3.zero);

        if (transform.position.y <= 0)
        {
            if (playSound)
            {
                nightSource.Play();
                daySource.Stop();
                playSound = false;
                for(int i=0; i<enemies.Length; i++)
                {
                    enemies[i].GetComponent<NavMeshAgent>().speed = inicialSpeed+1.5f; 
                }
                playerObject.GetComponent<PlayerHealth>().TakeDamage(1);
            }

        }
        else
        {
            if (playSound)
            {
                daySource.Play();
                nightSource.Stop();
                playSound = false;
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<NavMeshAgent>().speed = inicialSpeed;
                }
            }
        }

    }

    }
