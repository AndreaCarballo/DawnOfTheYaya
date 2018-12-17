using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DialogoController : MonoBehaviour {


    public GameObject paneldeDialogo;

    public Text dialogoNPC;

    public GameObject respuesta;

    private bool dialogoActivo = false;

    private float prevSpeed;

    private GameObject player;

    DialogoNPC dialogos;
    private GameObject posicionSalirTaxi;
    public GameObject playerobject;
    public GameObject Gatoobject;
    // Use this for initialization

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prevSpeed = player.GetComponent<NavMeshAgent>().speed;
        posicionSalirTaxi = GameObject.Find("PosicionSalirTaxi");
        playerobject = GameObject.FindWithTag("Player");
        Gatoobject = GameObject.Find("Gato");
    }

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
                player.GetComponent<NavMeshAgent>().speed = prevSpeed;
                ///En la posicion cerca del taxi pero sin chocar con el collider.
                if (DialogoTaxi.desactivado == true)
                {
                    playerobject.transform.position = new Vector3(posicionSalirTaxi.transform.position.x, 0, posicionSalirTaxi.transform.position.z);
                    playerobject.SetActive(true);
                    Gatoobject.transform.position = new Vector3(posicionSalirTaxi.transform.position.x, 0, posicionSalirTaxi.transform.position.z + 1);
                    Gatoobject.SetActive(true);

                }
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
        player.GetComponent<NavMeshAgent>().speed = 0f;
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
