using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAgent : MonoBehaviour {

    public GameObject player;
    public float walkingDistance = 1f; //speed
    public float xStartPosition = 0f;
    public float yStartPosition = 0.5f;
    public float zStartPosition = 4f;

    //position to restart the player for training
    public int xPlayer = 4;
    public int zPlayer = 4;

    //variables that save the previous player position
    private float lastDistance = 0f;


    Rigidbody rb;
    //private NavMeshAgent myNavMeshAgent;

    private bool positiveTrigger = false;
    private bool negativeTrigger = false;
    private bool restartPostion = false;

    QLearning QL = new QLearning(4); //4 possible actions

    public float positiveReward = 10f; //reward when it hits the player

    //uncomment when an object produces a negative reward
    //public float negativeReward = -10f;

    public float timePunishment = -1f; //punishment for each update
    public float distanceReward = 1f; //reward when it gets close

    private float currentReward = 0f;

    System.Random random = new System.Random();

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        //The state of the world is the relative position of the zombie to the player
        Vector3 relativePositionToPlayer = player.transform.InverseTransformPoint(transform.position);
        relativePositionToPlayer.x = (float)Math.Round((double)relativePositionToPlayer.x);
        relativePositionToPlayer.z = (float)Math.Round((double)relativePositionToPlayer.z);
        float[] qState = new float[] { relativePositionToPlayer.x, relativePositionToPlayer.z };

        //Debug.Log("qState  " + qState[0] + ", " + qState[1]);

        int action = QL.getAction(qState, currentReward); //gets the optimal action from q-learning
        currentReward = 0f; //reinitializes reward

        Move(action); //moves the zombie

        currentReward += timePunishment; //punishment for step

        if (positiveTrigger) //checks if it hit the player
        {
            currentReward += positiveReward;
        }

        //uncomment when there's an object that produces negative rewards
        //if (negativeTrigger)
        //{
        //    currentReward += negativeReward;
        //}

        
        float currentDistance = (float)Math.Round((double)Vector3.Distance(transform.position, player.transform.position));
        Debug.Log("before " + lastDistance + " after: " + currentDistance);

        if (currentDistance < lastDistance) //if the agent got closer to the player it'll get a reward
        {
            Debug.Log("closer");
            currentReward += distanceReward;
        }

        lastDistance = currentDistance;
        rb.velocity = Vector3.zero;

        if (restartPostion) //for training
        {
            transform.position = new Vector3(xStartPosition, yStartPosition, zStartPosition);
            player.transform.position = new Vector3(random.Next(-xPlayer, xPlayer), yStartPosition, random.Next(-zPlayer, zPlayer));
            restartPostion = false;
            //Debug.Log("restarting position");
        }
    }

    // detect collision with object
    void OnTriggerEnter(Collider col)
    {

        // reaction to positive reward
        if (col.gameObject.name == "Player")
        {
            positiveTrigger = true;
            restartPostion = true;
        }


        //uncomment when there's an object that produces negative rewards
        // reaction to negative reward
        //if (col.gameObject.name == "")
        //{
        //    negativeTrigger = true;
        //}

    }

    // detect collision with object
    void OnTriggerExit(Collider col)
    {
        // reaction to positive reward
        if (col.gameObject.name == "Player")
        {
            positiveTrigger = false;
        }


        //uncomment when there's an object that produces negative rewards
        // reaction to negative reward
        //if (col.gameObject.name == "")
        //{
        //    negativeTrigger = false;
        //}
    }

    void Move(int inputVal)
    {
        if (inputVal == 0) //up
        {
            rb.MovePosition(transform.position + transform.forward * walkingDistance);
        }
        if (inputVal == 1) //down
        {
            rb.MovePosition(transform.position + transform.forward * -walkingDistance);
        }
        if (inputVal == 2) //right
        {
            rb.MovePosition(transform.position + transform.right * walkingDistance);
        }
        if (inputVal == 3) //left
        {
            rb.MovePosition(transform.position + transform.right * -walkingDistance);
        }
        
    }
}
