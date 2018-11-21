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
    public GameObject player;

    //Private variables
    private Vector3 initialCamPos;

    // Use this for initialization
    void Start()
    {
        LivRoomCamera.enabled = true;
        GarageCamera.enabled = false;
        StairsCamera.enabled = false;
        KitchenCamera.enabled = false;
        GardenCamera.enabled = false;
        BathCamera.enabled = false;
        MainCamera.enabled = false;
        initialCamPos = MainCamera.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChangeCamera")
        {
            switch (other.name)
            {
                case "TargetGarage":
                    LivRoomCamera.enabled = false;
                    GarageCamera.enabled = true;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetBath":
                    LivRoomCamera.enabled = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = true;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom":
                    LivRoomCamera.enabled = true;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom2":
                    LivRoomCamera.enabled = true;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetLivRoom3":
                    LivRoomCamera.enabled = true;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetStairs":
                    LivRoomCamera.enabled = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = true;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetKitchen":
                    LivRoomCamera.enabled = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = true;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetKitchen2":
                    LivRoomCamera.enabled = false;
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = true;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = false;
                    break;
                case "TargetGarden":
                    LivRoomCamera.enabled = false;
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
                    GarageCamera.enabled = false;
                    StairsCamera.enabled = false;
                    KitchenCamera.enabled = false;
                    GardenCamera.enabled = false;
                    BathCamera.enabled = false;
                    MainCamera.enabled = true;
                    break;
                

                   
            }

        }
    }
}