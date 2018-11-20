using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour {

    #region Variables
    //Variables
    private ItemDatabase itemDatabase;
    private Inventory inventory;
    private bool showCraftSystem;
    private bool showToolTip;
    private string toolTip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;
    private SceneInteract sceneInteractScript;
    private bool craftItems;

    //Visible Variables
    public List<Item> slotsToCraft = new List<Item>();
    public List<Item> inventoryToCraft = new List<Item>();
    public int numberOfSlots;
    public Item itemCrafted = new Item();
    public bool goCraft;
    public GUISkin skin;

    #endregion

    // Use this for initialization
    void Start ()
    {
        sceneInteractScript = gameObject.GetComponent<SceneInteract>();

        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        //inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        for (int i = 0; i < numberOfSlots; i++)
        {
            slotsToCraft.Add(new Item());
            inventoryToCraft.Add(new Item());
        }

        AddItem(0);
        AddItem(2);
        draggingItem = false;
        showToolTip = false;

    }
	
	// Update is called once per frame
	void Update () {
        goCraft = sceneInteractScript.goCraft;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return"))
            craftItems = true;
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;

        if (goCraft)
        {
            DrawCraftSystem();
            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), toolTip);
        }

        if (draggingItem)
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);

        if (craftItems && goCraft)
        {
            CraftItems();
            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), toolTip);
        }
    }


    void DrawCraftSystem()
    {
        Event ec = Event.current;

        Rect backgroundRect = new Rect(375, 75, 200, 100);
        GUI.Box(backgroundRect, "CRAFT SYSTEM");

        for (int i = 0; i < numberOfSlots; i++)
        {
            Rect slotRect = new Rect(i * 30 + 400, 30 + 75, 25, 25);
            GUI.Box(slotRect, "", skin.GetStyle("Slot"));
            slotsToCraft[i] = inventoryToCraft[i];

            if (slotsToCraft[i].itemName != null)
            {
                GUI.DrawTexture(slotRect, slotsToCraft[i].itemIcon);
                if (slotRect.Contains(ec.mousePosition))
                {
                    toolTip = CreateToolTip(slotsToCraft[i]);
                    showToolTip = true;
                    if (ec.button == 0 && ec.type == EventType.MouseDrag && !draggingItem)
                    {
                        draggingItem = true;
                        prevIndex = i;
                        draggedItem = slotsToCraft[i];
                        inventoryToCraft[i] = new Item();
                    }
                    if (ec.type == EventType.MouseUp && draggingItem)
                    {
                        inventoryToCraft[prevIndex] = inventoryToCraft[i];
                        inventoryToCraft[i] = draggedItem;
                        draggingItem = false;
                        draggedItem = null;
                    }

                }
            }
            else
            {
                if (slotRect.Contains(ec.mousePosition))
                {
                    if (ec.type == EventType.MouseUp && draggingItem)
                    {
                        inventoryToCraft[i] = draggedItem;
                        draggingItem = false;
                        draggedItem = null;
                    }
                }

                if (!backgroundRect.Contains(ec.mousePosition) && ec.isMouse
                    && ec.type == EventType.MouseUp && draggingItem)
                {
                    draggingItem = false;
                    draggedItem = null;
                }
            }

                if (toolTip == "")
                    showToolTip = false;
        }

    }

    void CraftItems()
    {
        Rect slotRect = new Rect(525, 75 + 60, 30, 30);
        GUI.Box(slotRect, "", skin.GetStyle("Slot"));

        //Roasted Fruits
        if (SlotsToCraftContains(0)) //Apple
            if (SlotsToCraftContains(2)) //Banana
                itemCrafted = itemDatabase.myItemList[3];

        if(itemCrafted != null)
        GUI.DrawTexture(slotRect, itemCrafted.itemIcon);
        if (slotRect.Contains(Event.current.mousePosition))
        {
            toolTip = CreateToolTip(itemCrafted);
            showToolTip = true;

            if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                
            }
        }
    }



    string CreateToolTip(Item item)
    {
        toolTip = item.itemName + "\n\n<color=#fbd1ff>" + item.itemDescription + "</color>\n";
        return toolTip;
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventoryToCraft.Count; i++)
        {
            if (inventoryToCraft[i].itemName == null)
            {
                for (int j = 0; j < itemDatabase.myItemList.Count; j++)
                {
                    if (itemDatabase.myItemList[j].itemID == id)
                    {
                        inventoryToCraft[i] = itemDatabase.myItemList[j];
                    }
                }
                break;
            }
        }
    }

    bool SlotsToCraftContains(int id)
    {
        foreach (Item item in slotsToCraft)
        {
            if (item.itemID == id)
            {
                return true;
            }
        }
        return false;
    }

}
