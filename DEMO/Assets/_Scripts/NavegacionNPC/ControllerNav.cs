using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerNav : MonoBehaviour {

     public Transform objetivo;
   public NavMeshAgent mynav;
   
    void start()
    {
        if (mynav == null)
        {
            mynav = this.gameObject.GetComponent<NavMeshAgent>();

        }
    }
    void Update()
    {
        mynav.SetDestination(objetivo.position);
       
    }
}
