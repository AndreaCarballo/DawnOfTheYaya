using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //Variables
    public List<Item> myItemList = new List<Item>();

    private void Start()
    {
        myItemList.Add(new Item("apple", "asd", 2, 3, Item.ItemType.Food));

    }

}
