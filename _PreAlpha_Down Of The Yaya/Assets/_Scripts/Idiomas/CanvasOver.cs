using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOver : MonoBehaviour {
    public Text tgameover;
    public Text tmain;
    public Text tresstar;

	// Use this for initialization
	void Start () {
        if (LanguageManager.idioma == 0) {
            tgameover.text = "FIN DEL JUEGO";
            tmain.text = "MENU PRINCIPAL";
            tresstar.text = "JUGAR";


        }
        else if (LanguageManager.idioma == 1) {

            tgameover.text = "GAME OVER";
            tmain.text = "MAIN MENU";
            tresstar.text = "RESTART";


        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
