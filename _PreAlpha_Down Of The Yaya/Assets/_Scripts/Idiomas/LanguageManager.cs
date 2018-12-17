using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour {

    public Text topciones;
    public Text topciones1;
    public Text tplay;
    public Text tquit;
    public Text tload;
    public Text tlenguajes;
    public Text tlenguajes1;
    public Text tvolumen;


    

    public Text tespañol;
    public Text tingles;
    public Text tback;
    public Text tback1;


    //INICIALIZO EL IDIOMA A 0_ESPAÑOL 1-INGLES
    public static int idioma=0;

    LanguageSettings langSet;
   

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //langSet = new LanguageSettings();
        //langSet = JsonUtility.FromJson<LanguageSettings>(File.ReadAllText(Application.persistentDataPath + "/langSet.json"));
        //if (langSet.languages == 0)
        //{
        //    onClickEs();
        //}
        //else
        //{
        //    onClickEn();
        //}
    }

    public void onClickEs() {

        langSet = new LanguageSettings();
        
        langSet.languages = 0;
        idioma = 0;
        string jsonData = JsonUtility.ToJson(langSet,true);
        File.WriteAllText(Application.persistentDataPath + "/langSet.json", jsonData);
        

       
        tplay.text = "JUGAR";
        tload.text = "CARGAR";
        tlenguajes.text= "IDIOMAS";
        tlenguajes1.text = "IDIOMAS";
        topciones.text = "OPCIONES";
        topciones1.text = "OPCIONES";
        tquit.text = "SALIR";
        tvolumen.text = "VOLUMEN";
        tback.text = "VOLVER";
        tback1.text = "VOLVER";


        tespañol.text = "ESPAÑOL";
        tingles.text = "INGLES";

        
            
        
    }
    public void onClickEn()
    {

        langSet = new LanguageSettings();
        langSet.languages = 1;
        idioma = 1;
      
        
        string jsonData = JsonUtility.ToJson(langSet, true);
        File.WriteAllText(Application.persistentDataPath + "/langSet", jsonData);

       
        tplay.text = "PLAY";
        tload.text = "LOAD";
        tlenguajes.text = "LANGUAGES";
        tlenguajes1.text = "LANGUAGES";
        topciones.text = "OPTIONS";
        topciones1.text = "OPTIONS";
        tquit.text = "QUIT";
        tvolumen.text = "VOLUME";
        tback.text = "BACK";
        tback1.text = "BACK";



        tespañol.text = "SPANISH";
        tingles.text = "ENGLISH";



    }
   
}
