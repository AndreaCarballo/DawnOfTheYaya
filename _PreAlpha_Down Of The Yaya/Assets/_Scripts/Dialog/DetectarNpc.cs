using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DetectarNpc : MonoBehaviour
{
    [SerializeField] GameObject msgPanel;
    [SerializeField] Text msgText = null;


    GameObject actualNPC;
    private GameObject playerObject;

    // Use this for initialization
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        msgPanel.SetActive(false); //Inicianimos panel a falso
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "nPC")
        {
            actualNPC = other.gameObject;
            string msg = actualNPC.GetComponent<MensajeNPC>().GetMsg();

            msgText.text = msg;
            msgPanel.SetActive(true); //cuando choque con npc de tag salta el panel
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "nPC")
        {
            actualNPC.GetComponent<MensajeNPC>().Reiniciar();
            string msg = actualNPC.GetComponent<MensajeNPC>().GetMsg();

            msgText.text = msg;
            msgPanel.SetActive(false); //Cuando sale del triger el panel se pone a falso
        }
    }


}
