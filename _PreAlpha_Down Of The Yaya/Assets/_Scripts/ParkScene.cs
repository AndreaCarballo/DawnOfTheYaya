using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParkScene : MonoBehaviour
{

    private GameObject playerObject;

    public GameObject panelCanvas;
    private int i = 0;
    public Camera camera;
    public GameObject enemy;
    private float velocidad;
    public bool enddialogs;

    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        velocidad = playerObject.GetComponent<NavMeshAgent>().speed;
        enddialogs = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enddialogs)
        {
            playerObject.GetComponent<NavMeshAgent>().speed = velocidad;
            playerObject.GetComponent<CamaraSeguimiento>().enabled = true;
            i++;
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && i == 0)
        {
            panelCanvas.SetActive(true);
            playerObject.GetComponent<CamaraSeguimiento>().enabled = false;
            playerObject.GetComponent<NavMeshAgent>().speed = 0f;
            camera.transform.LookAt(enemy.transform.position);
            Time.timeScale = 0.2f;
            enddialogs = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelCanvas.SetActive(false);
            enddialogs = false;
        }
    }

}

