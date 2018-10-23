using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{

    #region Variables
    //Variables
    private Color firstColor;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    private GameObject playerObject;
    private bool activeInteract = false;

    //Visible Variables
    public Texture2D cursorTextureHand;
    public Inventory inventory;
    #endregion


    // Use this for initialization
    void Start()
    {
        firstColor = gameObject.GetComponent<Renderer>().material.color;
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerObject.transform.position, transform.position) <= 2 && activeInteract)
        {
            print("hey");
            
            activeInteract = false;
        }
    }

    #region Methods
    void OnMouseOver()
    {
        //Linearly interpolates between colors a and b by time.
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(firstColor,
            Color.yellow,
            Mathf.PingPong(Time.time, 1));

        //Change the cursor
        Cursor.SetCursor(cursorTextureHand, hotSpot, cursorMode);

        //Player movement if we press "left button mouse"
        if (Input.GetMouseButtonDown(0))
        {
            playerObject.GetComponent<MovClick>().SetTargetPositionAndMove();
            activeInteract = true;
        }
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = firstColor;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    #endregion
}
