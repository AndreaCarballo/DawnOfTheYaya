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

    //Visible Variables
    public Texture2D cursorTextureHand;
    public Inventory inventory;
    #endregion


    // Use this for initialization
    void Start()
    {
        firstColor = gameObject.GetComponent<Renderer>().material.color;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        prevSpeed = playerObject.GetComponent<NavMeshAgent>().speed;
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
                    break;
                case "Brocoli":
                    inventory.AddItemByID(1);
                    break;
                case "Bananas":
                    inventory.AddItemByID(2);
                    break;
            }

            activeInteract = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            Destroy(gameObject); 
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
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    #endregion
}
