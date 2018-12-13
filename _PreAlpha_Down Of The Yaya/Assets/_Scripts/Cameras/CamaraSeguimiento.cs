using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    //Public variables
    public GameObject player;
    public Camera mainCamera;

    //Private variables
    public Vector3 offset;
    private Vector3 DownPos;
    private Vector3 UpPos;
    private Vector3 RightPos;
    private Vector3 LeftPos;
    private bool mov1Y;
    private bool mov2Y;
    private bool mov1X;
    private bool mov2X;


    // Use this for initialization
    void Start()
    {
        offset = mainCamera.transform.position - player.transform.position;
        mov1Y = true;
        mov2Y = false;
        mov1X = true;
        mov2X = false;
    }

    private void Update()
    {
        DownPos = new Vector3((player.transform.position + offset).x,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z - 2.5f);
        UpPos = new Vector3((player.transform.position + offset).x,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z + 2.5f);
        RightPos = new Vector3((player.transform.position + offset).x -5f,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z);
        LeftPos = new Vector3((player.transform.position + offset).x + 5f,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                DownPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                LeftPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                UpPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                RightPos, 0.85f * Time.deltaTime);
        }
        else
        {
            RayCastCamera();
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                player.transform.position + offset, 1.0f * Time.deltaTime);
            
        }

        if (Input.GetKeyDown(KeyCode.Z) && mov1Y)
        {
            offset = new Vector3(-offset.x, offset.y, offset.z);
            mov1Y = false;
            mov2Y = true;
            mov1X = false;
            mov2X = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && mov2Y)
        {
            offset = new Vector3(offset.x, offset.y, -offset.z);
            mov2Y = false;
            mov1Y = true;
            mov2X = false;
            mov1X = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && mov1X)
        {
            offset = new Vector3(offset.x, offset.y, -offset.z);
            mov1X = false;
            mov2X = true;
            mov1X = false;
            mov2X = true;
        }
        else if (Input.GetKeyDown(KeyCode.X) && mov2X)
        {
            offset = new Vector3(-offset.x, offset.y, offset.z);
            mov2X = false;
            mov1X = true;
            mov2Y = false;
            mov1Y = true;
        }

        mainCamera.transform.LookAt(player.transform);
    }

    void RayCastCamera()
    {
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        LayerMask mask = LayerMask.GetMask("Roof");

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward)*30, Color.yellow);
        if (Physics.Raycast(ray, out hit,30,mask))
        {
            Transform objectHit = hit.transform;
            offset = new Vector3(-offset.x, offset.y, -offset.z);
        }
    }
}