using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoTaxi : MonoBehaviour {
    public DialogoNPC[] dialogo = new DialogoNPC[2];

    DialogoController dialogoController;

    public GameObject paneldeDialogo;
    public GameObject playerobject;
    public GameObject gatoobject;
    public GameObject taxiobject;

    public bool enOfDialog;
    public static bool desactivado;

    private bool objectTouched;

    // Use this for initialization
    void Start()
    {
        dialogoController = FindObjectOfType<DialogoController>();
        playerobject = GameObject.FindWithTag("Player");
        gatoobject = GameObject.Find("Gato");
    

        enOfDialog = false;
    }

    // Update is called once per frame
    void Update()
    {
        objectTouched = transform.GetComponent<Dialogable>().objectTouched;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectTouched && !enOfDialog)
        {

            ////////////////////AQUI PONGO LOS DIALOGOS NPC EN INGLES///////////////////////
            if (LanguageManager.idioma == 0)
            {
                dialogoController.ProximoDialogo(dialogo[0]);

                desactivado = true;
                playerobject.SetActive(false);
                gatoobject.SetActive(false);

                enOfDialog = true;

            }
            else if (LanguageManager.idioma == 1)
            {
                dialogoController.ProximoDialogo(dialogo[1]);

                desactivado = true;
                playerobject.SetActive(false);
                gatoobject.SetActive(false);

                enOfDialog = true;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            paneldeDialogo.SetActive(false);
            playerobject.SetActive(true);
            gatoobject.SetActive(true);
            objectTouched = false;
        }
    }


}
