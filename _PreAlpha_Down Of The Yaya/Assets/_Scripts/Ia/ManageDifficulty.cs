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

    private List<Prefabs>[][] objects;
    private int got0, got1, got2 = 0; //number of objects the player already got in each zone

    private PlayerHealth health;
    private Game control;

    //Objects to create
    public GameObject bottles;
    public GameObject apples;
    public GameObject bananas;
    public GameObject brocolis;
    public GameObject canes;
    public GameObject rocks;
    public Transform trans;
    private List<GameObject> currentObjects0 = new List<GameObject>();
    private List<GameObject> currentObjects1 = new List<GameObject>();
    private List<GameObject> currentObjects2 = new List<GameObject>();
    private Inventory inv;

    // Use this for initialization
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
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
                    got0 = 0;
                    got1 = 0;
                    got2 = 0;
                    control.setDifficultyToEasy();

                }
                else if (health.lifeLost > lifeThresholds[zone][control.difficulty - 1])
                {
                    got0 = 0;
                    got1 = 0;
                    got2 = 0;
                    control.setDifficultyToEasy();

                }
                break;
            case 2:
                if (time > timeThresholds[zone][control.difficulty - 1])
                {
                    got0 = 0;
                    got1 = 0;
                    got2 = 0;
                    control.setDifficultyToNormal();
                }
                else if (health.lifeLost > lifeThresholds[zone][control.difficulty - 1])
                {
                    got0 = 0;
                    got1 = 0;
                    got2 = 0;
                    control.setDifficultyToNormal();
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
                if (time < timeThresholds[zone][control.difficulty + 1])
                {
                    control.setDifficultyToHard();
                }
                else if (health.lifeLost < lifeThresholds[zone][control.difficulty + 1])
                {
                    control.setDifficultyToHard();
                }
                else if (time < timeThresholds[zone][control.difficulty])
                {
                    control.setDifficultyToNormal();
                }
                else if (health.lifeLost < lifeThresholds[zone][control.difficulty])
                {
                    control.setDifficultyToNormal();
                }
                break;
            case 1:
                if (time < timeThresholds[zone][control.difficulty])
                {
                    control.setDifficultyToHard();
                }
                else if (health.lifeLost < lifeThresholds[zone][control.difficulty])
                {
                    control.setDifficultyToHard();
                }
                break;
            case 2:
                break;
        }

    }


    public void objectsSet()
    {
        foreach (GameObject aux in currentObjects0)
        {
            if (aux != null)
            {
                Destroy(aux);
            }
            else
            {
                got0++;
            }

        }
        int need0 = objects[control.difficulty][zone].Count - got0; //number of objects needed
        currentObjects0 = new List<GameObject>();
        foreach (Prefabs ob in objects[control.difficulty][0])
        {
            if (need0 <= 0)
                break;
            GameObject a = Instantiate(ob.ob, ob.pos, Quaternion.identity, trans);
            ObjectInteract obin = a.GetComponent<ObjectInteract>();
            obin.inventory = inv;
            a.name = a.name.Split("("[0])[0];
            currentObjects0.Add(a);
            need0--;

        }

        foreach (GameObject aux in currentObjects1)
        {
            if (aux != null)
            {
                Destroy(aux);
            }
            else
            {
                got0++;
            }

        }
        int need1 = objects[control.difficulty][zone].Count - got1; //number of objects needed
        currentObjects1 = new List<GameObject>();
        foreach (Prefabs ob in objects[control.difficulty][1])
        {
            if (need1 <= 0)
                break;
            GameObject a = Instantiate(ob.ob, ob.pos, Quaternion.identity, trans);
            ObjectInteract obin = a.GetComponent<ObjectInteract>();
            obin.inventory = inv;
            a.name = a.name.Split("("[0])[0];
            currentObjects1.Add(a);
            need1--;

        }

        foreach (GameObject aux in currentObjects2)
        {
            if (aux != null)
            {
                Destroy(aux);
            }
            else
            {
                got0++;
            }

        }
        int need2 = objects[control.difficulty][zone].Count - got2; //number of objects needed
        currentObjects2 = new List<GameObject>();
        foreach (Prefabs ob in objects[control.difficulty][2])
        {
            if (need2 <= 0)
                break;
            GameObject a = Instantiate(ob.ob, ob.pos, Quaternion.identity, trans);
            ObjectInteract obin = a.GetComponent<ObjectInteract>();
            obin.inventory = inv;
            a.name = a.name.Split("("[0])[0];
            currentObjects2.Add(a);
            need2--;

        }


    }

    public void buildObjectList()
    {
        List<Prefabs> easy0 = new List<Prefabs>();
        easy0.Add(new Prefabs(bottles, new Vector3(0.7f, 0.7f, 1f)));
        easy0.Add(new Prefabs(bottles, new Vector3(2f, 0.2f, 5f)));
        easy0.Add(new Prefabs(bottles, new Vector3(0f, 0.2f, 5f)));
        easy0.Add(new Prefabs(bananas, new Vector3(1f, 0.7f, 1f)));

        List<Prefabs> normal0 = new List<Prefabs>();
        normal0.Add(new Prefabs(bottles, new Vector3(0.7f, 0.7f, 1f)));
        normal0.Add(new Prefabs(bottles, new Vector3(2f, 0.2f, 5f)));
        normal0.Add(new Prefabs(bananas, new Vector3(1f, 0.7f, 1f)));

        List<Prefabs> hard0 = new List<Prefabs>();
        hard0.Add(new Prefabs(bottles, new Vector3(0.7f, 0.7f, 1f)));
        hard0.Add(new Prefabs(bananas, new Vector3(1f, 0.7f, 1f)));

        List<Prefabs> easy1 = new List<Prefabs>();
        easy1.Add(new Prefabs(canes, new Vector3(45f, 0.2f, 0f)));
        easy1.Add(new Prefabs(apples, new Vector3(455f, 0.2f, 15f)));
        easy1.Add(new Prefabs(bottles, new Vector3(30f, 0.5f, -25f)));
        easy1.Add(new Prefabs(brocolis, new Vector3(50f, 0.2f, -30f)));

        List<Prefabs> normal1 = new List<Prefabs>();
        normal1.Add(new Prefabs(canes, new Vector3(45f, 0.2f, 0f)));
        normal1.Add(new Prefabs(apples, new Vector3(55f, 0.2f, 15f)));
        normal1.Add(new Prefabs(brocolis, new Vector3(50f, 0.2f, -30f)));

        List<Prefabs> hard1 = new List<Prefabs>();
        hard1.Add(new Prefabs(canes, new Vector3(45f, 0.2f, 0f)));
        hard1.Add(new Prefabs(brocolis, new Vector3(50f, 0.2f, -30f)));

        List<Prefabs> easy2 = new List<Prefabs>();
        easy2.Add(new Prefabs(apples, new Vector3(20f, 0.2f, -57f)));
        easy2.Add(new Prefabs(bottles, new Vector3(10f, 0.2f, -57f)));
        easy2.Add(new Prefabs(rocks, new Vector3(-30f, 0.2f, -56f)));
        easy2.Add(new Prefabs(bananas, new Vector3(-20f, 0.2f, -57f)));

        List<Prefabs> normal2 = new List<Prefabs>();
        normal2.Add(new Prefabs(apples, new Vector3(20f, 0.2f, -57f)));
        normal2.Add(new Prefabs(rocks, new Vector3(10f, 0.2f, -57f)));
        normal2.Add(new Prefabs(bananas, new Vector3(-20f, 0.2f, -57f)));

        List<Prefabs> hard2 = new List<Prefabs>();
        hard2.Add(new Prefabs(apples, new Vector3(20f, 0.2f, -57f)));
        hard2.Add(new Prefabs(bananas, new Vector3(-20f, 0.2f, -57f)));

        List<Prefabs>[] easyLists = new List<Prefabs>[] { easy0, easy1, easy2 };
        List<Prefabs>[] normalLists = new List<Prefabs>[] { normal0, normal1, normal2 };
        List<Prefabs>[] hardLists = new List<Prefabs>[] { hard0, hard1, hard2 };
        objects = new List<Prefabs>[][] { easyLists, normalLists, hardLists };

    }
}
