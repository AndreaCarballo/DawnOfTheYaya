using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInteract : MonoBehaviour
{

    #region Variables
    //Variables
    private Color firstColor;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    private GameObject playerObject;
    private bool activeInteract = false;
    private float prevSpeed;
    private GameObject lifeHUD;
    private GameObject mainCamera;
    private bool playSound;
    private Animator anim;

    //Visible Variables
    public Texture2D cursorTextureHand;
    public Texture2D cursorTexture;
    public Inventory inventory;
    public GameObject endPreAlphaMenu;
    public AudioClip pickClip;
    public GameObject taxiEndMusic;
    public GameObject ambientSound;
    #endregion


    // Use this for initialization
    void Start()
    {
        firstColor = gameObject.GetComponent<Renderer>().material.color;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        prevSpeed = playerObject.GetComponent<NavMeshAgent>().speed;
        lifeHUD = GameObject.Find("HealthUI");
        mainCamera = GameObject.Find("MainCamera");
        playSound = false;
        anim = playerObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerObject.transform.position, transform.position) <= 2 && activeInteract)
        {
            switch (gameObject.name)
            {
                case "Apple":
                    inventory.AddItemByID(0);
                    playSound = true;
                    anim.SetTrigger("PickObject");
                    break;
                case "Brocoli":
                    inventory.AddItemByID(1);
                    playSound = true;
                    anim.SetTrigger("PickObject");
                    break;
                case "Bananas":
                    inventory.AddItemByID(2);
                    playSound = true;
                    anim.SetTrigger("PickObject");
                    break;
                case "RoastedFruits":
                    inventory.AddItemByID(3);
                    playSound = true;
                    anim.SetTrigger("PickObject");
                    break;
                case "Taxi":
                    //End of PreAlpha
                    Destroy(playerObject);
                    //ambientSound.SetActive(false);
                    //taxiEndMusic.SetActive(true);
                    //endPreAlphaMenu.SetActive(true);
                    //lifeHUD.SetActive(false);
                    break;
                case "Bottle":
                    inventory.AddItemByID(4);
                    playSound = true;
                    anim.SetTrigger("PickObject");
                    break;
            }

            activeInteract = false;
            Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
            if(playSound)
                PlaySound();
            if (gameObject.name != "Taxi")
                Destroy(gameObject); 
        }
    }

    #region Methods

    void PlaySound()
    {
        playerObject.GetComponent<AudioSource>().PlayOneShot(pickClip,0.6f);
        playSound = false;
    }

    void OnMouseOver()
    {
        //Linearly interpolates between colors a and b by time.
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(firstColor,
            Color.yellow,
            Mathf.PingPong(Time.time, 1));

        //Change the cursor
        Cursor.SetCursor(cursorTextureHand,new Vector2 (hotSpot.x+10f,hotSpot.y-1f), cursorMode);

        //Player movement if we press "left button mouse"
        if (Input.GetMouseButtonDown(0))
        {
            playerObject.GetComponent<MovClick>().SetTargetPositionAndMove();
            activeInteract = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerObject.GetComponent<NavMeshAgent>().speed = 0f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerObject.GetComponent<NavMeshAgent>().speed = prevSpeed;
            playerObject.GetComponent<NavMeshAgent>().SetPath(new NavMeshPath());
        }     
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = firstColor;
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }
    #endregion
}
