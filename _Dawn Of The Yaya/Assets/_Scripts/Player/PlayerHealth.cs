using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour {

    public int Healthstart =100;
    public int currentHealth;
    public int cantidadRestaportiempo = 0;
    public Slider healthSlider;
    public Image damageImage;
    //public AudioClip death;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public GameObject MenuGameOver;
    public GameObject gato;
    bool isDead;
    bool damage;
    
    public GameObject gameOverSound;
    public GameObject ambientSound;
    public GameObject inventory;

    [HideInInspector]
    public int lifeLost = 0;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    private void Awake()
    {

        currentHealth = Healthstart; //inicianmos la vida a 100;

    }
    private void Update()
    {


    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; //decrementamos salud en la cantidad
        lifeLost += amount;
        healthSlider.value = currentHealth; //Actualizamos slider de vida
        if (currentHealth <= 0 && !isDead) {
            Death();
        }

    }
    public void TakeLife(int amount)
    {
        currentHealth += amount; //decrementamos salud en la cantidad
        healthSlider.value = currentHealth; //Actualizamos slider de vida
        if (currentHealth >= Healthstart)
        {
            currentHealth = Healthstart;
        }

    }

    void Death()
    {
        isDead = true; //jugador muerto
        //FindObjectOfType<AudioManagerGame>().Play("death");
        
        ambientSound.SetActive(false);
        gameOverSound.SetActive(true);
        MenuGameOver.SetActive(true);
        inventory.SetActive(false);
        //Destroy(this.gameObject, 0f);
        //Destroy(gato.gameObject, 0f);
    }


}
