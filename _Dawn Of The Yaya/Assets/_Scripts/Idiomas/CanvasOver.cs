using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOver : MonoBehaviour {
    public Text tgameover;
    public Text tmain;
    public Text tresstar;

    public Text tendlevel;
    public Text tmainenu;
    public Text tlevel2;

    // Use this for initialization
    void Start () {
        if (LanguageManager.idioma == 0) {
            tgameover.text = "FIN DEL JUEGO";
            tmain.text = "MENU PRINCIPAL";
            tresstar.text = "JUGAR";


        }
        else if (LanguageManager.idioma == 1)
        {

            tgameover.text = "GAME OVER";
            tmain.text = "MAIN MENU";
            tresstar.text = "RESTART";
            tendlevel.text = "So far the level of 'Dawn of the Yaya'.What will become of Lupe? Will the food be cold? And what will become of her grandson? Will she have enough warm? Be attentive to the development, and we await your feedback.";
            tmainenu.text = "MAIN MENU";
            tlevel2.text = "LEVEL 2";


        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
