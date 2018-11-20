using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

    #region Variables
    //Variables
    private ItemDatabase itemDatabase;
    private bool showInventory = false;
    private bool showToolTip;
    private string toolTip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;
    private GameObject craftStation;
    private bool goCraft;
    private GameObject playerObject;

    //Visible Variables
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public int slotsX, slotsY;
    public GUISkin skin;

    #endregion

    void Start()
    {
        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        craftStation = GameObject.Find("CraftStation");
        playerObject = GameObject.FindGameObjectWithTag("Player");

        for (int i=0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        //AddItemByID(0);
        //AddItemByID(1);
        //AddItemByID(2);
        //AddItemByID(3);
        //RemoveItemByID(0);

    }

    private void Update()
    {
        goCraft = craftStation.GetComponent<SceneInteract>().goCraft;

        if (Input.GetKeyDown(KeyCode.I))
        {
            draggingItem = false;
            showToolTip = false;
            showInventory = !showInventory;
        }
        
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            DrawInventory();
            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 300, 200), toolTip);
        }
            
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
        }

    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;

        Rect backgroundRect = new Rect(75, 75, slotsX * slotsY * (slotsX+1) + 65, slotsX * slotsY * (slotsY + 1) + 75);
        GUI.Box(backgroundRect, "INVENTORY");

        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(x * 30 + 100, y * 30 + 100, 25, 25);
                GUI.Box(slotRect, "", skin.GetStyle("Slot"));
                slots[i] = inventory[i];

                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect,slots[i].itemIcon);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;
                        if(e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item();
                        }
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1 && goCraft)
                        {
                            craftStation.GetComponent<CraftSystem>().AddItem(inventory[i]);
                            inventory[i] = new Item();
                        }
                        if (Input.GetKeyDown(KeyCode.U)) //Aumentar vida si pulsamos la U mientras tenemos el raton encima
                        {
                            playerObject.GetComponent<PlayerHealth>().TakeLife(inventory[i].itemPower);
                            inventory[i] = new Item();
                        }
                            
                    }

                } else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                    if (!backgroundRect.Contains(e.mousePosition) && e.isMouse
                        && e.type == EventType.MouseUp && draggingItem && goCraft)
                    {
                        craftStation.GetComponent<CraftSystem>().AddItem(draggedItem);
                        draggingItem = false;
                        draggedItem = null;
                    }

                    if (!backgroundRect.Contains(e.mousePosition) && e.isMouse 
                        && e.type == EventType.MouseUp && draggingItem)
                    {
                        draggingItem = false;
                        draggedItem = null;
                    }
                    
                }

                if (toolTip == "")
                    showToolTip = false;
                i++;
            }
        }
    }

    string CreateToolTip(Item item)
    {
        toolTip = item.itemName + "\n\n<color=#fbd1ff>" + item.itemDescription + "</color>\n";
        return toolTip;
    }

    public void  AddItemByID(int id)
    {
        for (int i=0;i<inventory.Count;i++)
        {
            if(inventory[i].itemName == null)
            {
                for (int j=0;j<itemDatabase.myItemList.Count;j++)
                {
                    if(itemDatabase.myItemList[j].itemID == id)
                    {
                        inventory[i] = itemDatabase.myItemList[j];
                    }
                }
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                inventory[i] = item;
                break;
            }
        }
    }

    void RemoveItemByID(int id)
    {
        for (int i=0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        foreach (Item item in inventory)
        {
            if (item.itemID == id)
            { 
                return true;
            }
        }
        return false;
    }

}
