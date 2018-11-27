using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour {

    public DialogoNPC[] dialogo = new DialogoNPC[2];

    DialogoController dialogoController;

    public GameObject paneldeDialogo;

    public bool enOfDialog;

    public GameObject player;

    private bool objectTouched;


    // Use this for initialization
    void Start () {
        dialogoController = FindObjectOfType<DialogoController>();
        enOfDialog = false;
        player = GameObject.FindGameObjectWithTag("Player");
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
            dialogoController.ProximoDialogo(dialogo[0]);
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
