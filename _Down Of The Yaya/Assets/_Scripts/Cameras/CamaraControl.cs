using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamaraControl : MonoBehaviour
{
    //Public variables
    public Camera MainCamera;
    public Camera inCamera;
    public Camera TaxiCamera;
    public GameObject player;
    public GameObject transitionTarget;
    public GameObject transitionTarget2;
    public GameObject paneltransicionTaxi;
    public GameObject paneltransicionEnemy;
    public GameObject paneltransicionMainCamera;

    //Private variables
    private Vector3 initialCamPos;
    private bool activateTransitionCamera;
    private bool activateTransitionCamera2;
    private bool activateTransitionCamera3;
    public bool CinematicDone;
    private bool CinematicDoing;
    private GameObject taxi;
    private Animator anim;
    private AudioSource audio;
    private GameObject[] objects;

    // Use this for initialization
    void Start()
    {
        inCamera.enabled = true;
        TaxiCamera.enabled = false;
        MainCamera.enabled = false;
        activateTransitionCamera = false;
        activateTransitionCamera2 = false;
        activateTransitionCamera3 = false;
        CinematicDone = false;
        CinematicDoing = false;
        objects = GameObject.FindGameObjectsWithTag("Transparent");
        taxi = GameObject.Find("Taxi");
        player = GameObject.FindGameObjectWithTag("Player");
        initialCamPos = TaxiCamera.transform.position;
        paneltransicionTaxi.SetActive(false);
        paneltransicionEnemy.SetActive(false);
        paneltransicionMainCamera.SetActive(false);
        anim = player.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (activateTransitionCamera && !CinematicDone)
        {
            TransitionTaxiCamera();
            if (Vector3.Distance(TaxiCamera.transform.position, transitionTarget.transform.position) <= 0.5)
            {
                activateTransitionCamera = false;
                activateTransitionCamera2 = true;
                paneltransicionTaxi.SetActive(false);
            }
            player.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (activateTransitionCamera2 && !CinematicDone)
        {
            TransitionEnemyCamera();
            if (Vector3.Distance(TaxiCamera.transform.position, transitionTarget2.transform.position) <= 0.5)
            {
                activateTransitionCamera2 = false;
                activateTransitionCamera3 = true;
                paneltransicionEnemy.SetActive(false);
            }
            player.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (activateTransitionCamera3 && !CinematicDone)
        {
            TransitionMainCamera();
            if (Vector3.Distance(TaxiCamera.transform.position, initialCamPos) <= 0.25 && CinematicDoing)
            {
                activateTransitionCamera3 = false;
                MainCamera.enabled = true;
                TaxiCamera.enabled = false;
                paneltransicionMainCamera.SetActive(false);
                CinematicDone = true;
            }
            player.GetComponent<NavMeshAgent>().isStopped = true;
        }

        if(MainCamera.enabled)
            player.GetComponent<NavMeshAgent>().isStopped = false;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChangeCamera")
        {
            switch (other.name)
            {
                case "TargetIN":
                    inCamera.enabled = true;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    MainCamera.enabled = false;
                    for(int i=0; i<objects.Length; i++)
                    {
                        objects[i].SetActive(false);
                    }
                    break;
                case "TargetOUT":
                    inCamera.enabled = true;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetExterior":
                    for (int i = 0; i < objects.Length; i++)
                    {
                        objects[i].SetActive(true);
                    }
                    MainCamera.transform.position = initialCamPos;
                    inCamera.enabled = false;
                    TaxiCamera.enabled = true;
                    activateTransitionCamera = true;
                    MainCamera.enabled = false;
                    if (CinematicDone)
                    {
                        paneltransicionTaxi.SetActive(false);
                        paneltransicionEnemy.SetActive(false);
                        paneltransicionMainCamera.SetActive(false);
                        MainCamera.enabled = true;
                        TaxiCamera.enabled = false;
                        activateTransitionCamera = false;
                    }
                    break;
    
            }
        }
    }


    void TransitionTaxiCamera()
    {
        anim.SetTrigger("IdleHuman");
        audio.Stop();
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                transitionTarget.transform.position, 0.35f * Time.deltaTime);
        paneltransicionTaxi.SetActive(true);
    }
    void TransitionEnemyCamera()
    {
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                transitionTarget2.transform.position, 0.45f * Time.deltaTime);
        paneltransicionEnemy.SetActive(true);
    }
    void TransitionMainCamera()
    {
        CinematicDoing = true;
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                initialCamPos, 0.45f * Time.deltaTime);
        paneltransicionMainCamera.SetActive(true);
    }

}