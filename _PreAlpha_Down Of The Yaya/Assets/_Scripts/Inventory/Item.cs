using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    //Variables
    public string itemName;
    public string itemDescription;
    public int itemID;
    public Texture2D itemIcon;
    public int itemPower;
    public ItemType itemType;

    public enum ItemType
    {
        Weapon,
        Food,
        Quest,
        Meds
    }

    //Weapon, Food and Med item constructor
    public Item(string name, string descript, int ID,int power, ItemType type)
    {
        itemName = name;
        itemID = ID;
        itemDescription = descript;
        itemIcon = Resources.Load<Texture2D>("Item Icons/"+itemName);
        itemPower = power;
        itemType = type;
    }

    // Empty item constructor
    public Item()
    {

    }

    // Quest item constructor
    public Item(string name, string descript, int ID, ItemType type)
    {
        itemName = name;
        itemID = ID;
        itemDescription = descript;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + itemName);
        itemType = type;
    }

}
