using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeNPC : MonoBehaviour {
  int indice =0;
    public  string[] msg = { };
   
    public string GetMsg()
    {
        return msg[indice];
    }
    public void BtnOk()
    {
        indice =1;

    }

    public void BtnCancel()
    {
        indice =2 ;
    }


    public void Reiniciar()
    {
        indice = 0;
    }
}
