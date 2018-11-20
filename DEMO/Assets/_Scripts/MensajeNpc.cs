using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeNpc : MonoBehaviour {
    int indice = 0;
    public string[] msg = {
        "Hola soy indice 1", //0
        "Soy indice 2",      //1
        "Hasta pronto" //2
    };


    public string GetMsg()
    {
        return msg[indice];
    }


    public void ButtonSi()
    {
        indice = 1;
    }
    public void ButtonNo()
    {
        indice = 0;
    }
}
