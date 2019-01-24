using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MantenerIdioma : MonoBehaviour {

    public  int idioma2;
    public Text tvolver;
    public Text tvolver1;
    public Text tvolver2;
    public Text tvolver3;
    public Text tmenuprincipal;
    public Text tcargar;
    public Text tguardar;
    public Text topciones;
    public Text topciones1;
    public Text tsalirjuego;
    public Text tcontroles;
    public Text tdificultad;
    public Text tdificultad1;
    public Text tfacil;
    public Text tnormal;
    public Text tdificil;

    public Text tpaneltaxi;
    public Text tpanelEnemy;
    public Text tpanelMain;




    // Use this for initialization
    void Start () {
        
        idioma2 = LanguageManager.idioma;
        //Debug.Log(" IDIOMA EN START DE MANTENER  " + idioma2);
        //Debug.Log(" IDIOMA EN START DE MANTENER con la variable de otro " + LanguageManager.idioma);
        if (LanguageManager.idioma == 0)
        {

            MenuESPAÑOL();

        }
        else if (LanguageManager.idioma == 1) {

            CambioPaneles();


        }

        
    }
	
	// Update is called once per frame
	void Update () {
      
	}
    public void MenuESPAÑOL()
    {

     tvolver.text="VOLVER";
     tvolver1.text="VOLVER";
     tvolver2.text="VOLVER";
      tvolver3.text="VOLVER";
      tmenuprincipal.text="MENU PRINCIPAL";
      tcargar.text="CARGAR JUEGO";
      tguardar.text="GUARDAR JUEGO";
      topciones.text="OPCIONES";
      topciones1.text="OPCIONES";
      tsalirjuego.text="SALIR DEL JUEGO";
      tcontroles.text="CONTROLES";
      tdificultad.text="DIFICULTAD";
      tdificultad1.text="DIFICULTAD";
      tfacil.text="FÁCIL";
      tnormal.text="NORMAL";
      tdificil.text="DÍFICIL";

        tpanelEnemy.text = "¿Y esos moinantes de ahí?, vaya pintas llevan esos mozos. ¿Qué pensarán sus padres?";
        tpanelMain.text = "Mejor ir aligerando, estoy segura que están pensando en robarnos Mistol!";
        tpaneltaxi.text = "Ay Jesús! está todo hecho un desastre. Andando no llego en la vida, igual ese taxi de ahí me puede acercar.";



    }

    public void CambioPaneles()
    {



        tpanelEnemy.text = "And those 'moinantes' of there ?, go pints those young men carry. What will your parents think ? ";
        tpanelMain.text = "Better go lightening, I'm sure they're thinking about stealing Mistol!";
        tpaneltaxi.text = "Oh Jesus! everything is a disaster. Walking did not come in life, just like that taxi there can bring me closer.";

    }
}
