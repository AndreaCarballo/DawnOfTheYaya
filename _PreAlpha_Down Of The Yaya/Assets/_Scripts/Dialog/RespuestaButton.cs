using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespuestaButton : MonoBehaviour {

    Respuesta respuestaData;

    public void ProximoDialogo()
    {
        FindObjectOfType<DialogoController>().ProximoDialogo(respuestaData.proximoDialogo);
    }

    public void Setup(Respuesta respuesta) //incializa el boton
    {
        respuestaData = respuesta;
    }
}
