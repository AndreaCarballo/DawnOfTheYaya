using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovClick : MonoBehaviour {

    #region Variables
    //Variables
    private NavMeshAgent myNavMeshAgent;
    private Vector3 setTarget;
    private Rigidbody myRigidbody;
    private GameObject mGameObject;
    private float previousVelocity;
    private Animator anim;
    private Vector3 goal;


    //Visible Variables
    public bool sawIntroduction;
    #endregion


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.angularSpeed = 360;
        previousVelocity = myNavMeshAgent.speed;
        sawIntroduction = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.x == goal.x && transform.position.z == goal.z)
        {
            anim.SetTrigger("IdleHuman");
        }
        if (Input.GetMouseButton(1))
            SetTargetPositionAndMove();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            myNavMeshAgent.speed = myNavMeshAgent.speed/2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            myNavMeshAgent.speed = myNavMeshAgent.speed*2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myNavMeshAgent.speed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myNavMeshAgent.speed = previousVelocity;
            StopMovement();
        }

    }

    void OnGUI()
    {
        if(!Input.GetKey(KeyCode.F1) && !sawIntroduction)
        {
            myNavMeshAgent.speed = 0f;
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 55);
            GUI.Box(backgroundRect, "\n Por favor, antes de empezar mantenga pulsado F1 para acceder a los controles de juego");
        }
        if (Input.GetKey(KeyCode.F1))
        {
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 3, 925, 300);
            GUI.Box(backgroundRect,
            "<color=#fff>" + "\n\nCONTROLES DE JUGADOR:"+ "</color>" + "\nMovimiento con" + " <color=#ce5100>" + "click derecho" + "</color>"
            + "\nInteracción (Atacar, coger objetos e interactuar con el entorno) " + "<color=#00ce4f>" + "click izquierdo" + "</color>"
            + "\nMarcar opciones de diálogo con " + "<color=#00ce4f>" + "click izquierdo" + "</color>"
            + "\nAcceder al inventario con la letra " + "<color=#00bdce>" + "\"I\" "+"</color>"
            + "\nMovimiento en sigilo con la tecla " + " <color=#00bdce>" + "\"Shift\" "+"</color>"
            + "\nDetener el movimiento con la tecla " + " <color=#00bdce>" + "\"Espacio\" " + "</color>"
            + "<color=#fff>" + "\n\nCONTROLES DE CÁMARA (en exteriores):" + "</color>" 
            + "\nPara rotar hacia la derecha pulsar la tecla " + " <color=#00bdce>" + "\"D\" " + "</color>"
            + "\nPara rotar hacia la izquierda pulsar la tecla " + " <color=#00bdce>" + "\"A\" " + "</color>"
            + "\nPara ascender la vista pulsar la tecla " + " <color=#00bdce>" + "\"W\" " + "</color>"
            + "\nPara descender la vista pulsar la tecla " + " <color=#00bdce>" + "\"S\" " + "</color>"
            + "\nPara rotar la vista al ángulo complementario pulsar la tecla " + " <color=#00bdce>" + "\"X\" " + "</color>"
            + "\nPara cambiar el ángulo de vista pulsar la tecla " + " <color=#00bdce>" + "\"Z\" " + "</color>");
            myNavMeshAgent.speed = previousVelocity;
            sawIntroduction = true;
        }
    }

    #region Methods
    public void StopMovement()
    {
        myNavMeshAgent.SetPath(new NavMeshPath());
        anim.SetTrigger("WalkHuman");
    }
    public void SetTargetPositionAndMove()
    {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            setTarget = hit.point;
            myNavMeshAgent.SetDestination(setTarget);
            goal = setTarget;
            anim.SetTrigger("WalkHuman");
        }
    }
    #endregion

}
