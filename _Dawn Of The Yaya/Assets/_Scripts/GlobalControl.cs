using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;
    public Slider healthBarSlider;  //reference for slider
    public Canvas HUD;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(healthBarSlider);
            DontDestroyOnLoad(HUD);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
