using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTexto : MonoBehaviour {
  

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.F)) //Con F mostramos Debug por pantalla
        {
           
            this.transform.GetComponent<Text>().text = "Objetos"+ GameObject.FindGameObjectsWithTag("Objeto").Length;
            //Lo que este tageado como Objeto lo cuenta !!

            //GameObject.Find("DebugText").SetActive(!GameObject.Find("DebugText").activeInHierarchy);
        }
	}
}
