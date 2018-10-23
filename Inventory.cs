using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Variables
    //Variables
    private ItemDatabase itemDatabase;
    private bool showInventory = false;

    //Visible Variables
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public int slotsX, slotsY;
    public GUISkin skin;

    #endregion

    void Start()
    {
        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        for(int i=0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;
        }
        
    }

    void OnGUI()
    {

        if (showInventory)
            DrawInventory();

        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10, 10, 200, 50), inventory[i].itemName);
        }
    }

    void DrawInventory()
    {
        for(int x = 0; x < slotsX; x++)
        {
            for(int y = 0; y < slotsY; y++)
            {
                GUI.Box(new Rect(x * 20, y * 20, 20, 20), y.ToString());
            }
        }
    }

}
