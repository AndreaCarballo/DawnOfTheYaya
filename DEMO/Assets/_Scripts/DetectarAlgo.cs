using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectarAlgo : MonoBehaviour {

   [SerializeField] GameObject msgPanel1;
    [SerializeField] Text msgText;
    [SerializeField] Button buttonSi;
    [SerializeField] Button buttonNo;
    // Use this for initialization

    GameObject actualNpc;


    void Start () {
        GetComponent<Collider>().isTrigger = true;
        msgPanel1.SetActive(false);
	}
	

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "npc") {
            actualNpc = other.gameObject;
            string msg = actualNpc.GetComponent<MensajeNpc>().GetMsg();
            msgText.text = msg;
            
            msgPanel1.SetActive(true);
                }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name == "npc")
        {
            msgPanel1.SetActive(false);
        }
    }

    public void ButtonSi()
    {
       
            Debug.Log("Botton si entra");
            actualNpc.GetComponent<MensajeNpc>().ButtonSi();

            string msg = actualNpc.GetComponent<MensajeNpc>().GetMsg();
            msgText.text = msg;

       
    }
    public void ButtonNo()
    {
      
            Debug.Log("Botton no entra");
            actualNpc.GetComponent<MensajeNpc>().ButtonNo();
            string msg = actualNpc.GetComponent<MensajeNpc>().GetMsg();
            msgText.text = msg; //actualizar el texto mostrado
        
    }

}
