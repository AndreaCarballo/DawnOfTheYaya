using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDifficulty : MonoBehaviour
{

    [HideInInspector]
    public int zone = 0;
    [HideInInspector]
    public float time = 0f;

    private List<int[]> lifeThresholds = new List<int[]>();
    private List<float[]> timeThresholds = new List<float[]>();

    private PlayerHealth health;
    private Game control;

    // Use this for initialization
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        control = GameObject.Find("Canvas").GetComponent<Game>();
        int[] life0 = new int[2] { 60, 20 };
        int[] life1 = new int[2] { 80, 40 };
        int[] life2 = new int[2] { 70, 30 };
        lifeThresholds.Add(life0);
        lifeThresholds.Add(life1);
        lifeThresholds.Add(life2);
        float[] time0 = new float[2] { 6f * 60f, 3f * 60f };
        float[] time1 = new float[2] { 7f * 60f, 4f * 60f };
        float[] time2 = new float[2] { 6.5f * 60f, 3.5f * 60f };
        timeThresholds.Add(time0);
        timeThresholds.Add(time1);
        timeThresholds.Add(time2);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch (control.difficulty)
        {
            case 0:
                break;
            case 1:
                if (time > timeThresholds[zone][control.difficulty - 1])
                {
                    control.difficulty--;
                }
                else if (health.lifeLost > lifeThresholds[zone][control.difficulty - 1])
                {
                    control.difficulty--;
                }
                break;
            case 2:
                if (time > timeThresholds[zone][control.difficulty - 1])
                {
                    control.difficulty--;
                }
                else if (health.lifeLost > lifeThresholds[zone][control.difficulty - 1])
                {
                    control.difficulty--;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // reaction to positive reward
        if (col.gameObject.tag == "CheckPoint")
        {
            string[] name = col.gameObject.name.Split(" "[0]);
            applyZone();
            zone = int.Parse(name[1]);

        }

    }


    void OnTriggerExit(Collider col)
    {
        // reaction to positive reward
        if (col.gameObject.tag == "CheckPoint")
        {
            time = 0;
            health.lifeLost = 0;
            Destroy(col.gameObject);

        }

    }

    void applyZone()
    {

        switch (control.difficulty)
        {
            case 0:
                if (time < timeThresholds[zone][control.difficulty])
                {
                    control.difficulty++;
                }
                else if (health.lifeLost < lifeThresholds[zone][control.difficulty])
                {
                    control.difficulty++;
                }
                break;
            case 1:
                if (time < timeThresholds[zone][control.difficulty])
                {
                    control.difficulty++;
                }
                else if (health.lifeLost < lifeThresholds[zone][control.difficulty])
                {
                    control.difficulty++;
                }
                break;
            case 2:
                break;
        }

    }
}
