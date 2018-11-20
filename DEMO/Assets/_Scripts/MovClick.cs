using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovClick : MonoBehaviour {

    #region Variables
    //Variables
    private NavMeshAgent myNavMeshAgent;
    private Vector3 setTarget;
    private Rigidbody myRigidbody;
    private GameObject mGameObject;

    //Visible Variables
    #endregion


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.angularSpeed = 360;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(1))
            SetTargetPositionAndMove();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            myNavMeshAgent.speed = myNavMeshAgent.speed/2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            myNavMeshAgent.speed = myNavMeshAgent.speed*2;
        if (Input.GetKeyDown(KeyCode.Space))
            myNavMeshAgent.isStopped=true;
        if (Input.GetKeyUp(KeyCode.Space))
            myNavMeshAgent.isStopped=false;

    }

    #region Methods
    public void SetTargetPositionAndMove()
    {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            setTarget = hit.point;
            myNavMeshAgent.SetDestination(setTarget);
        }
    }
    #endregion

}
