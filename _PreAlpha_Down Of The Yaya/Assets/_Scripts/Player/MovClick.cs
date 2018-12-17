using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovClick : MonoBehaviour
{

    #region Variables
    //Variables
    private NavMeshAgent myNavMeshAgent;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector3 setTarget;
    private Rigidbody myRigidbody;
    private GameObject mGameObject;
    private float previousVelocity;
    private Animator anim;
    private Vector3 goal;
    private AudioSource audio;
    private bool stealth = false;
    public GameObject pauseMenuUI;
    public GameObject panelOptions;
    public GameObject panelControls;



    //Visible Variables
    public Texture2D cursorTexture;
    public bool sawIntroduction;
    public AudioClip steps;
    public GameObject panelControlsObject;
    #endregion


    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.angularSpeed = 360;
        previousVelocity = myNavMeshAgent.speed;
        sawIntroduction = false;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.clip = steps;
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == goal.x && transform.position.z == goal.z)
        {
            anim.SetTrigger("IdleHuman");
            audio.Stop();
        }
        if (Input.GetMouseButton(1))
            SetTargetPositionAndMove();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myNavMeshAgent.speed = myNavMeshAgent.speed / 2;
            audio.Stop();
            stealth = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myNavMeshAgent.speed = myNavMeshAgent.speed * 2;
            audio.Play();
            stealth = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            myNavMeshAgent.speed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myNavMeshAgent.speed = previousVelocity;
            StopMovement();
        }

        if (Input.GetKey(KeyCode.F1))
        {
            panelControlsObject.SetActive(true);
            myNavMeshAgent.speed = previousVelocity;
            sawIntroduction = true;
        }else
        {
            panelControlsObject.SetActive(false);
        }

    }

    void OnGUI()
    {
        if (!Input.GetKey(KeyCode.F1) && !sawIntroduction && !pauseMenuUI.activeSelf && !panelOptions.activeSelf && !panelControls.activeSelf )
        {
            myNavMeshAgent.speed = 0f;
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 55);
            if (LanguageManager.idioma == 0)
            {

 GUI.Box(backgroundRect, "\n Por favor, antes de empezar mantenga pulsado F1 para acceder a los controles de juego");

            }else if (LanguageManager.idioma == 1)
            {
                GUI.Box(backgroundRect, "\n Please, before you start hold down F1 to access the game controls");

            }
           
        }

    }

    #region Methods
    public void StopMovement()
    {
        myNavMeshAgent.SetPath(new NavMeshPath());
        anim.SetTrigger("WalkHuman");
        if (!audio.isPlaying && !stealth)
        {
            audio.Play();
        }
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
            if (!audio.isPlaying && !stealth)
            {
                audio.Play();
            }

        }
    }
    #endregion

}
