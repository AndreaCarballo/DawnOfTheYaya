using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovtoClick : MonoBehaviour {
    NavMeshAgent nv;
    Vector3 objetivo;
    // Use this for initialization
    void Start () {
        nv = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                objetivo = hit.point;
                SetPosition();
            }
        }
    }

    void SetPosition()
    {
        nv.SetDestination(objetivo);
    }
}
