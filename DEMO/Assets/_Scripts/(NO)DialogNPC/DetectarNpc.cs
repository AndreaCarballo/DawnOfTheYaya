using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectarNpc : MonoBehaviour
{
  [SerializeField] GameObject msgPanel;
    [SerializeField] Text msgText=null;
    [SerializeField] Button btnOk;
    [SerializeField] Button btnCancel;


    GameObject actualNPC;

    // Use this for initialization
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        msgPanel.SetActive(false); //Inicianimos panel a falso
    }

   
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "npc")
        {
            actualNPC = other.gameObject;
            string msg = actualNPC.GetComponent<MensajeNPC>().GetMsg();

            msgText.text = msg;
            msgPanel.SetActive(true); //cuando choque con npc de tag salta el panel
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "npc")
        {
            actualNPC.GetComponent<MensajeNPC>().Reiniciar();
            string msg = actualNPC.GetComponent<MensajeNPC>().GetMsg();

            msgText.text = msg;
            msgPanel.SetActive(false); //Cuando sale del triger el panel se pone a falso
        }
    }

    public void BtnOk()
    {
        actualNPC.GetComponent<MensajeNPC>().BtnOk();
        string msg = actualNPC.GetComponent<MensajeNPC>().GetMsg();

        msgText.text = msg;


    }
    public void BtnCancel()
    {
        actualNPC.GetComponent<MensajeNPC>().BtnCancel();
       string msg= actualNPC.GetComponent<MensajeNPC>().GetMsg();
        msgText.text = msg;

    }
}
