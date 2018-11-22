
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

   
    public string name;
    public AudioMixerGroup audioMixerGroup;
    public AudioClip clip;

    public bool loop;
    //[Range(1f,10f)]
    //public float time;
     
    [Range(0f,1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
