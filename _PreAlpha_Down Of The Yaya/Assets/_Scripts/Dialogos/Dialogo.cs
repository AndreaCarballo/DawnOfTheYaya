using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour {

    public DialogoNPC[] dialogo = new DialogoNPC[2];

    private bool dialogoacabado = false;

    DialogoController dialogoController;

    public GameObject paneldeDialogo;

    public bool enOfDialog;


    // Use this for initialization
    void Start () {
        dialogoController = FindObjectOfType<DialogoController>();
        enOfDialog = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
     void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (!dialogoacabado)
            {
                dialogoController.ProximoDialogo(dialogo[0]);
            }
            else
            {
                dialogoController.ProximoDialogo(dialogo[1]);

            }dialogoacabado = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            paneldeDialogo.SetActive(false);
            enOfDialog = true;
        }
    }


}
