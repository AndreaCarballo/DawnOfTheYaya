using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NuevaIso : MonoBehaviour {

    public GameObject Camara;
    public GameObject PosicionCamara;
    Vector3 objetivo;
    NavMeshAgent nv;
    // Use this for initialization
    void Start () {
        nv = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 0.1f));
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -0.1f));
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    PosicionCamara.transform.SetParent(null);
                    transform.Rotate(new Vector3(0, 5.0f, 0));
                    PosicionCamara.transform.SetParent(transform);
                }

                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        PosicionCamara.transform.SetParent(null);
                        transform.Rotate(new Vector3(0, -5.0f, 0));
                        PosicionCamara.transform.SetParent(transform);
                    }
                }
            }


        }Camara.transform.position = Vector3.Lerp(Camara.transform.position, PosicionCamara.transform.position, 0.1f);
       
    }
    

    }

