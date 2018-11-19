using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamaraControl : MonoBehaviour
{

    public Camera mainCamera;
    public Camera camera2;
    public GameObject player;
    public Transform setposition;


    // Use this for initialization
    void Start()
    {
        mainCamera.enabled = true;
        camera2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cambiocamera")
        {

            mainCamera.enabled = false;
            camera2.enabled = true;
            player.GetComponent<NavMeshAgent>().Warp(new Vector3(-88.102f, -0.766f, 0.324f));


        }

    }
}