using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //Variables
    public List<Item> myItemList = new List<Item>();

    private void Start()
    {
        myItemList.Add(new Item("apple", "AppleDescription", 0, 3, Item.ItemType.Food));
        myItemList.Add(new Item("broccoli", "broccoliDescription", 1, 3, Item.ItemType.Food));
        myItemList.Add(new Item("bananas", "bananasDescription", 2, 3, Item.ItemType.Food));

    }

}
