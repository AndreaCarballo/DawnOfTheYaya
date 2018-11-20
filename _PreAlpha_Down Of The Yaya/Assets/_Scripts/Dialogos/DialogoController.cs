using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoController : MonoBehaviour {


    public GameObject paneldeDialogo;

    public Text dialogoNPC;

    public GameObject respuesta;

    private bool dialogoActivo = false;

    DialogoNPC dialogos;
	// Use this for initialization
	

    
    void Update()
      
    {
    
		if(Input.GetMouseButtonDown(0) && dialogoActivo)
        {
            if (dialogos.respuestas.Length > 0)
            {
                MostrarRespuestas();
            }
            else
            {
                dialogoActivo = false;
                paneldeDialogo.SetActive(false);
                dialogoNPC.gameObject.SetActive(false);
                
               
            }
        }
	}
    void MostrarRespuestas()
    {
        dialogoNPC.gameObject.SetActive(false);
        dialogoActivo = false;
        for (int i=0; i< dialogos.respuestas.Length; i++)
        {
            GameObject tempRespuesta = Instantiate(respuesta, paneldeDialogo.transform) as GameObject;
            tempRespuesta.GetComponent<Text>().text = dialogos.respuestas[i].respuesta;
            tempRespuesta.GetComponent<RespuestaButton>().Setup(dialogos.respuestas[i]);

        }
    }


    public void ProximoDialogo(DialogoNPC dialogo)
    {
        dialogos = dialogo;
        QuitarRespuestas();

        dialogoActivo = true;
        paneldeDialogo.SetActive(true);
        dialogoNPC.gameObject.SetActive(true);

        dialogoNPC.text = dialogos.dialogo;
    }

    void QuitarRespuestas()
    {
        RespuestaButton[] buttons = FindObjectsOfType<RespuestaButton>();
        foreach(RespuestaButton button in buttons)
        {
            Destroy(button.gameObject); //A cada boton que encontramos lo limpiamos.
        }
    }

}
