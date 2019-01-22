using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInteract : MonoBehaviour
{

    #region Variables
    //Variables
    private Color firstColor;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    private GameObject playerObject;
    private bool activeInteract = false;
    private GameObject meshObject;
    private float prevSpeed;

    //Visible Variables
    public Texture2D cursorTextureHand;
    public Texture2D cursorTexture;
    #endregion


    // Use this for initialization
    void Start()
    {
        meshObject = GameObject.Find("female_genericMesh"); 
        firstColor = meshObject.GetComponentInChildren<Renderer>().material.GetColor("_Color");
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color",Color.red);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        prevSpeed = playerObject.GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerObject.transform.position, transform.position) <= 2 && activeInteract)
        {
            //print("hey");
            //Aqui va el codigo que queremos para tratar la accion con el enemigo
            activeInteract = false;
        }
    }

    #region Methods
    void OnMouseOver()
    {
        //Linearly interpolates between colors a and b by time.
        meshObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.Lerp(firstColor,
            new Color32 (255,42,30,1),
            Mathf.PingPong(Time.time, 0.5f));

        //Change the cursor
        Cursor.SetCursor(cursorTextureHand, hotSpot, cursorMode);

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
        meshObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = firstColor;
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }
    #endregion
}
