using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManagerGame : MonoBehaviour {

    public Sound[] sounds;


	// Use this for initialization
	void Awake () {
        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            //s.source.time = s.time;
           
            //Para cada s del string sounds le añadimos el componente 
            // audiosource
        }
		
	}
     void Start()
    {
        //si quiero un sonido de tema del juego
        //Play("Nombre del Sound")
    }
   
    public void Play(string name)
    {
        Sound s= Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
        //*******Para reproducir un sonido poner en el script que se quiera reproducir
        //******FindObjectOfType<AudioManager>().Play("Nombre del sound que se puso en el inspertos de sound")
        //***Hay una prueba de audio con clip en la funcion Death()
        
    }
    

}
