﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosPatrulla : MonoBehaviour {


    public Transform siguientePunto;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<ZombieAgent>().objetivo = siguientePunto; //cambiarle el objetivo a other que en este caso es npc
        }
    }
}
