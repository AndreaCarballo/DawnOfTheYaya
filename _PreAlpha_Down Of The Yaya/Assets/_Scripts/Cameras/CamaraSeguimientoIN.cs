using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimientoIN : MonoBehaviour
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
    private bool mov1A;
    private bool mov2A;
    private bool mov1D;
    private bool mov2D;


    // Use this for initialization
    void Start()
    {
        offset = mainCamera.transform.position - player.transform.position;
        mov1A = true;
        mov2A = false;
        mov1D = true;
        mov2D = false;
    }

    private void Update()
    {
        DownPos = new Vector3((player.transform.position - offset).x,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z + 1.5f);
        UpPos = new Vector3((player.transform.position + offset).x,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z + 1.5f);
        RightPos = new Vector3((player.transform.position + offset).x - 2.5f,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z);
        LeftPos = new Vector3((player.transform.position + offset).x + 2.5f,
            (player.transform.position + offset).y,
            (player.transform.position + offset).z);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //if (Input.GetKey(KeyCode.S))
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
        //        DownPos, 0.85f * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
        //        LeftPos, 0.85f * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.W))
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
        //        UpPos, 0.85f * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
        //        RightPos, 0.85f * Time.deltaTime);
        //}
        //else
        //{
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                player.transform.position + offset, 1.0f * Time.deltaTime);

        //}

        if (Input.GetKeyDown(KeyCode.A) && mov1A)
        {
            offset = new Vector3(-offset.x, offset.y, offset.z);
            mov1A = false;
            mov2A = true;
            mov1D = false;
            mov2D = true;
        } else if (Input.GetKeyDown(KeyCode.D) && mov1D)
        {
            offset = new Vector3(offset.x, offset.y, -offset.z);
            mov1A = false;
            mov2A = true;
            mov1D = false;
            mov2D = true;

        } else if (Input.GetKeyDown(KeyCode.A) && mov2A)
        {
            offset = new Vector3(offset.x, offset.y, -offset.z);
            mov1A = true;
            mov2A = false;
            mov1D = true;
            mov2D = false;
        } else if (Input.GetKeyDown(KeyCode.D) && mov2D)
        {
            offset = new Vector3(-offset.x, offset.y, offset.z);
            mov1A = true;
            mov2A = false;
            mov1D = true;
            mov2D = false;
        }

        mainCamera.transform.LookAt(player.transform);
    }
}