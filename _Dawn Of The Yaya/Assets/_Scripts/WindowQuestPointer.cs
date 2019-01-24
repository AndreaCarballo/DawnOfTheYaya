using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowQuestPointer : MonoBehaviour {

    //Private variables
    private RectTransform pointerRectTransform;
    private RectTransform squareRectTransform;
    private GameObject pointer;
    private GameObject square;
    private GameObject target;
    private GameObject player;
    private bool flag1;
    private bool flag2;
    private bool flag3;
    private bool flag4;
    //Public variables
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;

    private void Awake()
    {
        pointerRectTransform = GameObject.Find("Pointer").GetComponent<RectTransform>();
        squareRectTransform = GameObject.Find("SquarePointer").GetComponent<RectTransform>();
        pointer = GameObject.Find("Pointer");
        square = GameObject.Find("SquarePointer");
        player = GameObject.Find("Player");
        flag1 = false;
        flag2 = false;
        flag3 = false;
        flag4 = false;
    }

    private void Update()
    {
        flag1 = player.GetComponent<Tutorial>().EndTuto;
        flag2 = target2.GetComponent<DialogoTaxi>().enOfDialog;
        flag3 = target3.GetComponent<ParkScene>().endDialog;

        if (!flag1 && !flag2 && !flag3 && !flag4)
        {
            target = target1;
        }
        else if (flag1 && !flag2 && !flag3 && !flag4)
        {
            target = target2;
        }
        else if (flag1 && flag2 && !flag3 && !flag4)
        {
            target = target3;
        }
        else if (flag1 && flag2 && flag3 && !flag4)
        {
            target = target4;
        }
        else
        {
            target = target4;
        }
    }

    private void LateUpdate()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

        if(screenPos.z>0  && //OnScreen
            screenPos.x>0 && screenPos.x<Screen.width &&
            screenPos.y>0 && screenPos.y<Screen.height)
        {
            Debug.Log(screenPos);
            squareRectTransform.position = screenPos;
            square.SetActive(true);
            pointer.SetActive(false);
        }
        else //Offscreen
        {
            square.SetActive(false);
            pointer.SetActive(true);

            Debug.Log(screenPos);

            if (screenPos.z < 0) //for objects behind
                screenPos.z *= -1;

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0)/2;

            // Making 00 the center instead of bottom left
            screenPos -= screenCenter;

            // Find angle from center of screen to mouse position
            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            angle -= 90 * Mathf.Deg2Rad;

            //Debug.Log(angle);

            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenPos = screenCenter + new Vector3(sin*150, cos*150, 0);

            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            // Check up and down first
            if (cos>0)
            {
                screenPos = new Vector3(screenBounds.y/m, screenBounds.y, 0);
            }
            else //down
            {
                screenPos = new Vector3(-screenBounds.y/m, -screenBounds.y, 0);
            }

            //if out of bounds, get point on apprpiate side
            if (screenPos.x>screenBounds.x) // out of bounds must be on the right
            {
                screenPos = new Vector3(screenBounds.x, screenBounds.x*m, 0);
            }
            else if (screenPos.x<-screenBounds.x) //out of bounds left
            {
                screenPos = new Vector3(-screenBounds.x, -screenBounds.x*m, 0);
            } // else in bounds

            //remove coordinate translation
            screenPos += screenCenter;

            pointerRectTransform.position = screenPos;
            pointerRectTransform.transform.rotation = Quaternion.Euler(0, 0, angle*Mathf.Rad2Deg);
        }
    }

}
