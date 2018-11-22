using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //Variables
    public List<Item> myItemList = new List<Item>();

    private void Start()
    {
        myItemList.Add(new Item("apple", "Una manzana en buen estado." +"\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
            "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
            0, 3, Item.ItemType.Food));

        myItemList.Add(new Item("broccoli", "Una hortaliza en buen estado." +"\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
            "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
            1, 3, Item.ItemType.Food));

        myItemList.Add(new Item("bananas", "Un plátano en buen estado." + "\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
            "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
            2, 3, Item.ItemType.Food));

        myItemList.Add(new Item("roastedFruits", "Frutas horneadas." + "\n" + "Un alimento muy nutritivo que restaurará\n más salud que un ingrediente común" +
            "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
            3, 10, Item.ItemType.Food));

    }

}
