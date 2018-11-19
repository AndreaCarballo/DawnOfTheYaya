﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneInteract : MonoBehaviour
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
    public bool goCraft;
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
        if (Input.GetKeyDown(KeyCode.Escape) && !activeInteract || 
            (Vector3.Distance(playerObject.transform.position, transform.position) >= 2))
        {
            goCraft = false;
            playerObject.GetComponent<NavMeshAgent>().isStopped = false;
        }

        if (Vector3.Distance(playerObject.transform.position, transform.position) <= 2 && activeInteract)
        {
            playerObject.GetComponent<NavMeshAgent>().isStopped = true;
            goCraft = true;
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
