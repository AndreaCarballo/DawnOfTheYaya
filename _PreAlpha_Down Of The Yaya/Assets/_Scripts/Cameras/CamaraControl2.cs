using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl2 : MonoBehaviour {

    public Camera camera2;
    public Camera camera3;
    public GameObject player;
    public GameObject panelhud;
    public GameObject GameOverHUD;


    // Use this for initialization
    void Start()
    {
        camera2.enabled = true;
        camera3.enabled = false;



    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "findejuego")
        {

            camera2.enabled = false;
            camera3.enabled = true;
            panelhud.gameObject.SetActive(false);
            GameOverHUD.gameObject.SetActive(true);

        }

    }
}