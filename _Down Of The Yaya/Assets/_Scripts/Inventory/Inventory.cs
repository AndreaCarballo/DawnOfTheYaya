using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inventory : MonoBehaviour
{

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
    private Animator anim;
    private bool canThrow;
    private GameObject bottleSoundSource;
    private GameObject rockSoundSource;
    private int initialAtackDamage;



    //Visible Variables
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public Item itemWeapon = new Item();
    public int slotsX, slotsY;
    public GUISkin skin;
    public GUISkin skin2;
    public GameObject bottleGameObject;
    public GameObject sneakerGameObject;
    public GameObject rockGameObject;
    public GameObject caneGameObject;
    public GameObject eatAudioSource;

    #endregion

    void Start()
    {
        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        craftStation = GameObject.Find("CraftStation");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        initialAtackDamage = playerObject.GetComponent<PlayerAttack>().Damage;
        anim = playerObject.GetComponent<Animator>();
        canThrow = false;
        bottleSoundSource = GameObject.Find("CrashBottle");
        rockSoundSource = GameObject.Find("CrashRock");

        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
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

        if (itemWeapon != null || itemWeapon.itemName != "")
        {
            equipWeapon();

            if (Input.GetKeyDown(KeyCode.U))
            {
                if (itemWeapon.itemType == Item.ItemType.Food)
                {
                    anim.SetTrigger("Eat");
                    eatAudioSource.GetComponent<AudioSource>().Play();
                    playerObject.GetComponent<PlayerHealth>().TakeLife(itemWeapon.itemPower);
                    itemWeapon = new Item();
                }
            }

        }

        if (canThrow)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ThrowItem();
            }
        }

    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;

        DrawWeapon();

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

    void DrawWeapon()
    {
        Event e = Event.current;
        Rect slotWeapon = new Rect(Screen.width / 1.25f, Screen.height / 1.5f, 64, 64);
        GUI.Box(slotWeapon, "", skin2.GetStyle("Slot"));

        if (itemWeapon.itemName != null)
        {
            GUI.DrawTexture(new Rect(Screen.width / 1.25f + 15f, Screen.height / 1.5f + 15f, 32, 32), itemWeapon.itemIcon); //TODO cuidado aquí con el size del icon
            if (slotWeapon.Contains(e.mousePosition))
            {
                toolTip = CreateToolTip(itemWeapon);
                if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    draggedItem = itemWeapon;
                    quitWeapon();
                    itemWeapon = new Item();
                }
                if (e.isMouse && e.type == EventType.MouseDown && e.button == 1 && !goCraft)
                {
                    //playerObject.GetComponent<NavMeshAgent>().SetPath(new NavMeshPath()); //BUG parar movimiento, no funciona
                    AddItem(itemWeapon);
                    quitWeapon();
                    itemWeapon = new Item();
                }
            }

        }
        else
        {
            if (slotWeapon.Contains(e.mousePosition))
            {
                if (e.type == EventType.MouseUp && draggingItem)
                {
                    itemWeapon = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
            if (!slotWeapon.Contains(e.mousePosition) && e.isMouse
                && e.type == EventType.MouseUp && draggingItem && !goCraft)
            {
                AddItem(draggedItem);
                draggingItem = false;
                draggedItem = null;
            }

        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;

        Rect backgroundRect = new Rect(75, 75, slotsX * slotsY * (slotsX + 1) + 65, slotsX * slotsY * (slotsY + 1) + 75);
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
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;
                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
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
                            anim.SetTrigger("Eat");
                            eatAudioSource.GetComponent<AudioSource>().Play();
                            playerObject.GetComponent<PlayerHealth>().TakeLife(inventory[i].itemPower);
                            inventory[i] = new Item();
                        }
                        if (Input.GetKeyDown(KeyCode.E)) //Equipar Objeto
                        {
                            AddItem(itemWeapon);
                            quitWeapon();
                            itemWeapon = inventory[i];
                            inventory[i] = new Item();
                        }
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1 && !goCraft)
                        {
                            //playerObject.GetComponent<NavMeshAgent>().SetPath(new NavMeshPath()); //BUG parar movimiento, no funciona
                            AddItem(itemWeapon);
                            itemWeapon = inventory[i];
                            inventory[i] = new Item();
                        }
                    }

                }
                else
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

    #region Inventory Methods
    string CreateToolTip(Item item)
    {
        toolTip = item.itemName + "\n\n<color=#fbd1ff>" + item.itemDescription + "</color>\n";
        return toolTip;
    }

    public void AddItemByID(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for (int j = 0; j < itemDatabase.myItemList.Count; j++)
                {
                    if (itemDatabase.myItemList[j].itemID == id)
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

    public void RemoveItemByID(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    public bool InventoryContains(int id)
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
    #endregion

    #region Weapon Methods

    public void equipWeapon()
    {
        if (itemWeapon.itemType == Item.ItemType.Quest && itemWeapon != null)
        {
            if (itemWeapon.itemName == "bottle")
                bottleGameObject.SetActive(true);
            if (itemWeapon.itemName == "rock")
                rockGameObject.SetActive(true);
            canThrow = true;
        }
        if (itemWeapon.itemType == Item.ItemType.Weapon && itemWeapon != null)
        {
            if (itemWeapon.itemName == "sneaker")
                sneakerGameObject.SetActive(true);
            if (itemWeapon.itemName == "cane")
                caneGameObject.SetActive(true);

            playerObject.GetComponent<PlayerAttack>().Damage = initialAtackDamage + itemWeapon.itemPower;
        }

    }

    public void quitWeapon()
    {
        if (itemWeapon.itemType == Item.ItemType.Quest)
        {
            if (itemWeapon.itemName == "bottle")
                bottleGameObject.SetActive(false);
            if (itemWeapon.itemName == "rock")
                rockGameObject.SetActive(false);
        }

        if (itemWeapon.itemType == Item.ItemType.Weapon)
        {
            if (itemWeapon.itemName == "sneaker")
                sneakerGameObject.SetActive(false);
            if (itemWeapon.itemName == "cane")
                caneGameObject.SetActive(false);


            playerObject.GetComponent<PlayerAttack>().Damage = initialAtackDamage;
        }


        canThrow = false;
    }

    public void ThrowItem()
    {
        if (itemWeapon.itemName == "bottle")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 setTarget = hit.point;
                Vector3 direction = setTarget - bottleGameObject.transform.position;

                GameObject copyBottle = Instantiate(bottleGameObject);
                copyBottle.transform.position = bottleGameObject.transform.position;
                bottleGameObject.SetActive(false);

                copyBottle.transform.parent = null;
                copyBottle.GetComponent<Rigidbody>().useGravity = true;
                copyBottle.GetComponent<Rigidbody>().AddForce(direction * 85);
                copyBottle.GetComponent<Rigidbody>().AddForce(transform.up * 250);
                itemWeapon = new Item();

                bottleSoundSource.transform.position = setTarget;
                bottleSoundSource.GetComponent<AudioSource>().PlayDelayed(1);

            }
        }

        if (itemWeapon.itemName == "rock")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 setTarget = hit.point;
                Vector3 direction = setTarget - rockGameObject.transform.position;

                GameObject copyRock = Instantiate(rockGameObject);
                copyRock.transform.position = rockGameObject.transform.position;
                rockGameObject.SetActive(false);

                copyRock.transform.parent = null;
                copyRock.GetComponent<Rigidbody>().useGravity = true;
                copyRock.GetComponent<Rigidbody>().AddForce(direction * 85);
                copyRock.GetComponent<Rigidbody>().AddForce(transform.up * 250);
                itemWeapon = new Item();

                rockSoundSource.transform.position = setTarget;
                rockSoundSource.GetComponent<AudioSource>().PlayDelayed(1);

            }
        }

        canThrow = false;
    }
    #endregion

}
