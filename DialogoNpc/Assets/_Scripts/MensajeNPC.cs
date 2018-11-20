using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeNPC : MonoBehaviour {
  int indice =0;
    int impar=1;
    int par=2;
    public  string[] msg = { };
   
    public string GetMsg()
    {
        return msg[indice];
    }
    //public string GetMsgNo()
    //{
    //    return msg[indice];
    //}
    public void BtnOk()
    {
        indice +=impar;

    }

    public void BtnCancel()
    {
        indice +=par ;
    }


    public void Reiniciar()
    {
        indice = 0;
    }
}
