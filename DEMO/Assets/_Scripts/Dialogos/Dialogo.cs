using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour {

    public DialogoNPC[] dialogo = new DialogoNPC[2];

    private bool dialogoacabado = false;

    DialogoController dialogoController;
    


    // Use this for initialization
    void Start () {
        dialogoController = FindObjectOfType<DialogoController>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
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
  
    
   
}
