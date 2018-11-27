using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private GameObject player;
    private bool goCraft;
    private Item itemCrafted = null;
    private bool playSoundCraft;

    //Visible Variables
    public List<Item> slotsToCraft = new List<Item>();
    public List<Item> inventoryToCraft = new List<Item>();
    public int numberOfSlots;
    public GUISkin skin;
    public AudioClip soundCraft;

    #endregion

    // Use this for initialization
    void Start ()
    {
        sceneInteractScript = gameObject.GetComponent<SceneInteract>();

        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < numberOfSlots; i++)
        {
            slotsToCraft.Add(new Item());
            inventoryToCraft.Add(new Item());
        }

        //AddItemByID(0);
        //AddItemByID(2);
        draggingItem = false;
        showToolTip = false;
        craftItems = false;
        playSoundCraft = false;
    }
	
	// Update is called once per frame
	void Update () {
        goCraft = sceneInteractScript.goCraft;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return") && goCraft)
        {
            craftItems = true;
        }
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;

        if (goCraft)
        {
            DrawCraftSystem();
            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 200f, Event.current.mousePosition.y, 300, 200), toolTip);
        }

        if (draggingItem)
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);

        if (craftItems && goCraft)
        {
            CraftItems();
            if (itemCrafted != null && playSoundCraft)
            {
                transform.GetComponent<AudioSource>().PlayOneShot(soundCraft, 0.7f);
                playSoundCraft = false;
            }
                
            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 200f, Event.current.mousePosition.y, 300, 200), toolTip);
        }
    }


    void DrawCraftSystem()
    {
        Event ec = Event.current;

        Rect backgroundRect = new Rect(375, 75, 300, 275);
        GUI.Box(backgroundRect, "CRAFT SYSTEM" + "\n\n\n\n\n\n\n" + "<color=#fb8cff>"+
            " Para craftear ingredientes arrástralos\n desde tu inventario y pulsa 'Enter'"+ "</color>"
            + "\n\n<color=#fb8cff>" + " Puedes acelerar el proceso haciendo\n click derecho sobre los ingredientes\n" + "</color>" 
            + "\n\n<color=#fb8cff>" + " Para salir del sistema de crafteo\n pulsa 'Escape'" + "</color>");

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
                    if (ec.isMouse && ec.type == EventType.MouseDown && ec.button == 1)
                    {
                        inventory.AddItem(inventoryToCraft[i]);
                        inventoryToCraft[i] = new Item();
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
                    inventory.AddItem(draggedItem);
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
        if (InventoryToCraftContains(0) && InventoryToCraftContains(2)) //Apple and Banana
        {
            itemCrafted = itemDatabase.myItemList[3];
            playSoundCraft = true;
        }
            

        if(itemCrafted != null)
        {
            GUI.DrawTexture(slotRect, itemCrafted.itemIcon);
            switch (itemCrafted.itemID)
            {
                case 3: //Roasted Fruits
                    RemoveItemByID(0); //Apple
                    RemoveItemByID(2); //Banana
                    break;
            }
            
            if (slotRect.Contains(Event.current.mousePosition))
            {
                toolTip = CreateToolTip(itemCrafted);
                showToolTip = true;

                if (Event.current.isMouse && Event.current.type == EventType.MouseDown && Event.current.button == 1)
                {
                    inventory.AddItem(itemCrafted);
                    craftItems = false;
                    itemCrafted = null;
                }
            }
        }
        
    }



    string CreateToolTip(Item item)
    {
        toolTip = item.itemName + "\n\n<color=#fbd1ff>" + item.itemDescription + "</color>\n";
        return toolTip;
    }

    public void AddItemByID(int id)
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

    void RemoveItemByID(int id)
    {
        for (int i = 0; i < inventoryToCraft.Count; i++)
        {
            if (inventoryToCraft[i].itemID == id)
            {
                inventoryToCraft[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventoryToCraft.Count; i++)
        {
            if (inventoryToCraft[i].itemName == null)
            {
                inventoryToCraft[i] = item;
                break;
            }
        }
    }

    bool InventoryToCraftContains(int id)
    {
        for (int i = 0; i < inventoryToCraft.Count; i++)
        {
            if (inventoryToCraft[i].itemID == id)
            {
                return true;
            }
        }
        return false;
    }

}
