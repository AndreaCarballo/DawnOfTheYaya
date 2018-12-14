using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tutorial : MonoBehaviour {

    #region Variables
    //Variables
    private NavMeshAgent myNavMeshAgent;
    private Vector3 setTarget;
    private Rigidbody myRigidbody;
    private GameObject player;
    private GameObject telefono;
    private float previousVelocity;
    private bool helpPlayer;
    private bool EndTuto;
    private Inventory inventory;
    private GameObject MeshLinkExterior;
    private GameObject ColliderExteriorD;
    private GameObject ColliderExterior;
    private int contHelp = 0;
    public GameObject pauseMenuUI;
    public GameObject panelOptions;
    public GameObject panelControls;
    public GameObject panelLanguage;


    //Visible Variables
    public bool sawIntroduction;
    public bool objectTouched;
    public bool endOfDialog;
    
    public 
    #endregion

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        telefono = GameObject.Find("Telefono");
        myNavMeshAgent = player.GetComponent<NavMeshAgent>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        MeshLinkExterior = GameObject.Find("LivRoomExterior");
        ColliderExteriorD = GameObject.Find("ColliderExteriorD");
        ColliderExterior = GameObject.Find("ColliderExterior");
        objectTouched = false;
        endOfDialog = false;
        helpPlayer = false;
        EndTuto = false;
        MeshLinkExterior.SetActive(false);
    }

    void Update () {
        sawIntroduction = player.GetComponent<MovClick>().sawIntroduction;
        objectTouched = telefono.GetComponent<Dialogable>().objectTouched;
        endOfDialog = telefono.GetComponent<Dialogo>().enOfDialog;
    }

    void OnGUI()
    {
        if (sawIntroduction && !objectTouched &&!pauseMenuUI.activeSelf && !panelOptions.activeSelf && !panelControls.activeSelf && !panelLanguage.activeSelf)
        {
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 75);
            GUI.Box(backgroundRect, "\n Debería coger el teléfono y llamar a mi nieto, hace mucho que no sé de él. Últimamente la juventud anda más alocada de lo normal,\n" +
                " no hay más que ver la televisión.");
            helpPlayer = false;
            EndTuto = false;
            contHelp = 0;
        }

        if (objectTouched && endOfDialog && !helpPlayer && !EndTuto && !pauseMenuUI.activeSelf && !panelOptions.activeSelf && !panelControls.activeSelf && !panelLanguage.activeSelf)
        {
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 75);
            GUI.Box(backgroundRect, "\n Seguro que se quedó dormido y no fue a trabajar, será gandul!. Vamos minino! hay que llevarle una tarta, seguro que le sienta bien una visita.\n" +
                "Ay este muchacho.. con lo mal que tengo yo las verticales.");
        }

        if(helpPlayer && contHelp <= 2 && endOfDialog && !EndTuto && !pauseMenuUI.activeSelf && !panelOptions.activeSelf && !panelControls.activeSelf && !panelLanguage.activeSelf)
        {
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 55);
            GUI.Box(backgroundRect, "\n Mistol filliño, casi me olvido.. Deberíamos hacer la tarta para Maikel");
        }

        if (helpPlayer && contHelp > 2 && endOfDialog && !EndTuto && !pauseMenuUI.activeSelf && !panelOptions.activeSelf && !panelControls.activeSelf && !panelLanguage.activeSelf)
        {
            Rect backgroundRect = new Rect(Screen.width / 8, Screen.height / 1.25f, 925, 55);
            GUI.Box(backgroundRect, "\n Para hacer la tarta, si no recuerdo mal, necesitaba plátanos y manzanas.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitCollider"))
        {
            objectTouched = false;
            if(inventory.InventoryContains(3))
            {
                MeshLinkExterior.SetActive(true);
                Destroy(ColliderExteriorD);
                Destroy(ColliderExterior);
                EndTuto = true;
            }
            else
            {
                MeshLinkExterior.SetActive(false);
                contHelp++;
                helpPlayer = true;
            }            
        }
    }
}
