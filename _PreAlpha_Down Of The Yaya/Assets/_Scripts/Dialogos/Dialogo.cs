﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour {

    public DialogoNPC[] dialogo = new DialogoNPC[2];

    DialogoController dialogoController;

    public GameObject paneldeDialogo;

    public bool enOfDialog;

    private bool objectTouched;

    public AudioClip cogerClip;
    public AudioClip beepClip;
    public AudioClip colgarClip;

    // Use this for initialization
    void Start () {
        dialogoController = FindObjectOfType<DialogoController>();
        enOfDialog = false;
    }
	
	// Update is called once per frame
	void Update () {
        objectTouched = transform.GetComponent<Dialogable>().objectTouched;
	}


     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectTouched)
        { 
            enOfDialog = false;
            ////////////////////AQUI PONGO LOS DIALOGOS NPC EN INGLES///////////////////////
            if(LanguageManager.idioma == 0)
            {
                dialogoController.ProximoDialogo(dialogo[0]);

            }else if (LanguageManager.idioma == 1)
            {
                dialogoController.ProximoDialogo(dialogo[1]);

            }
            
            if (transform.gameObject.name == "Telefono")
            {
               
                if (LanguageManager.idioma == 0)
                {
                    dialogoController.ProximoDialogo(dialogo[0]);
                    transform.GetComponent<AudioSource>().PlayOneShot(cogerClip, 0.5f);
                    transform.GetComponent<AudioSource>().PlayOneShot(beepClip, 0.6f);


                }
                else if (LanguageManager.idioma == 1) {

                    dialogoController.ProximoDialogo(dialogo[1]);
                    transform.GetComponent<AudioSource>().PlayOneShot(cogerClip, 0.5f);
                    transform.GetComponent<AudioSource>().PlayOneShot(beepClip, 0.6f);

                }

                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            paneldeDialogo.SetActive(false);
            enOfDialog = true;
            if (transform.gameObject.name == "Telefono")
            {
                transform.GetComponent<AudioSource>().PlayOneShot(colgarClip, 0.5f);
            }
        }
    }


}
