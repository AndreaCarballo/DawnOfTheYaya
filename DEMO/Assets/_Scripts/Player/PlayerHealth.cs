using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int Healthstart =100;
    public int currentHealth;
    public int cantidadRestaportiempo = 0;
     public Slider healthSlider;
    public Image damageImage;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool isDead;
    bool damage;


    private void Awake()
    {

        currentHealth = Healthstart; //inicianmos la vida a 100;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Vamos a hacer un poco de imagen de daño
        
    }

    public void TakeDamage(int amount)
    {
       
        currentHealth -= amount; //decrementamos salud en la cantidad
        healthSlider.value = currentHealth;
        //Actualizamos slider de vida
        if (currentHealth <= 0 && !isDead) {
            Death();
        }

    }
    void Death()
    {
        isDead = true; //jugador muerto

    }


}
