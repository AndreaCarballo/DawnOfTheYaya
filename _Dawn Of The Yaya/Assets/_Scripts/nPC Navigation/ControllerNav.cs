using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerNav : MonoBehaviour
{

    public Transform objetivo;
    public NavMeshAgent mynav;
    private Animator anim;
    private bool stealth = false;

    void Start()
    {
        if (mynav == null)
        {
            mynav = this.gameObject.GetComponent<NavMeshAgent>();

        }
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (mynav.velocity.x == 0 && mynav.velocity.z == 0)
        {
            anim.SetTrigger("Idle");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                mynav.speed = mynav.speed / 2;
                stealth = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                mynav.speed = mynav.speed * 2;
                stealth = false;
            }
            if (stealth)
            {
                anim.SetTrigger("Stealth");
            }
            else
            {
                anim.SetTrigger("Walk");
            }
        }
        mynav.SetDestination(objetivo.position);

    }
}
