using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    //Public variables
    public GameObject player;

    //Private variables
    public Vector3 offset;
    private Vector3 DownPos;
    private Vector3 UpPos;
    private Vector3 RightPos;
    private Vector3 LeftPos;


    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
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
            transform.position = Vector3.Lerp(transform.position,
                DownPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.Lerp(transform.position,
                LeftPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.Lerp(transform.position,
                UpPos, 0.85f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position,
                RightPos, 0.85f * Time.deltaTime);
        }
        else
        {
            RayCastCamera();
            transform.position = Vector3.Lerp(transform.position,
                player.transform.position + offset, 1.0f * Time.deltaTime);
            
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            offset = new Vector3(-offset.x,offset.y,offset.z);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            offset = new Vector3(-offset.x, offset.y, -offset.z);
        }

        transform.LookAt(player.transform);
    }

    void RayCastCamera()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        LayerMask mask = LayerMask.GetMask("Roof");

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*30, Color.yellow);
        if (Physics.Raycast(ray, out hit,30,mask))
        {
            Transform objectHit = hit.transform;
            offset = new Vector3(-offset.x, offset.y, -offset.z);
            print(objectHit);
        }
    }
}