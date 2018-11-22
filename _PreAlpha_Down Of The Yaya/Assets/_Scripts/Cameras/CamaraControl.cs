using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamaraControl : MonoBehaviour
{
    //Public variables
    public Camera MainCamera;
    public Camera GarageCamera;
    public Camera LivRoomCamera;
    public Camera KitchenCamera;
    public Camera GardenCamera;
    public Camera BathCamera;
    public Camera StairsCamera;
    public Camera TaxiCamera;
    public GameObject player;
    public GameObject transitionTarget;
    public GameObject transitionTarget2;

    //Private variables
    private Vector3 initialCamPos;
    private bool activateTransitionCamera;
    private bool activateTransitionCamera2;
    private bool activateTransitionCamera3;
    private bool CinematicDone;
    private GameObject taxi;

    // Use this for initialization
    void Start()
    {
        LivRoomCamera.enabled = true;
        GarageCamera.enabled = false;
        TaxiCamera.enabled = false;
        StairsCamera.enabled = false;
        KitchenCamera.enabled = false;
        GardenCamera.enabled = false;
        BathCamera.enabled = false;
        MainCamera.enabled = false;
        activateTransitionCamera = false;
        activateTransitionCamera2 = false;
        activateTransitionCamera3 = false;
        CinematicDone = false;
        initialCamPos = MainCamera.transform.position;
        taxi = GameObject.Find("Taxi");
        player = GameObject.FindGameObjectWithTag("Player");
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
            }
            player.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (activateTransitionCamera3 && !CinematicDone)
        {
            TransitionMainCamera();
            if (Vector3.Distance(TaxiCamera.transform.position, MainCamera.transform.position) <= 0.25)
            {
                activateTransitionCamera3 = false;
                MainCamera.enabled = true;
                TaxiCamera.enabled = false;
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
                case "TargetGarage":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = true;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetBath":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = true;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom":
                    LivRoomCamera.enabled = true;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom2":
                    LivRoomCamera.enabled = true;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom3":
                    LivRoomCamera.enabled = true;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetStairs":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = true;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetKitchen":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = true;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetKitchen2":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = true;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetGarden":
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = false;
                    activateTransitionCamera = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = true;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetExterior":
                    MainCamera.transform.position = initialCamPos;
                    LivRoomCamera.enabled = false;
                    TaxiCamera.enabled = true;
                    activateTransitionCamera = true;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    if (CinematicDone)
                    {
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
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                transitionTarget.transform.position, 0.35f * Time.deltaTime);
    }
    void TransitionEnemyCamera()
    {
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                transitionTarget2.transform.position, 0.45f * Time.deltaTime);
    }
    void TransitionMainCamera()
    {
        TaxiCamera.transform.position = Vector3.Lerp(TaxiCamera.transform.position,
                MainCamera.transform.position, 0.45f * Time.deltaTime);
    }

}