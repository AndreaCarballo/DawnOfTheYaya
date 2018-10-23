using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform myTarget;
    public float smoothPositionFactor;
    public float smoothRotationFactor;
    // public Transform myPlayer;
    private Vector3 offsetPosition;
    // Use this for initialization
    void Start()
    {
        offsetPosition = transform.position - myTarget.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 idealPosition;

        idealPosition = myTarget.position + myTarget.rotation * offsetPosition;

        //transform.position = idealPosition;
        transform.position = Vector3.Lerp(transform.position, idealPosition, Time.deltaTime * smoothPositionFactor);


        Quaternion lookRotation = Quaternion.LookRotation(myTarget.forward, myTarget.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothRotationFactor);

        // transform.LookAt(myPlayer);
        // transform.rotation = lookRotation;
        //transform.forward = myTarget.transform.forward;


    }
}
