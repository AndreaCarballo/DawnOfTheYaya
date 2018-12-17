using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //Variables
    public List<Item> myItemList = new List<Item>();

    private void Start()
    {
        if (LanguageManager.idioma == 0)
        {



            myItemList.Add(new Item("apple", "Una manzana en buen estado." + "\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
                      "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
                      0, 3, Item.ItemType.Food));

            myItemList.Add(new Item("broccoli", "Una hortaliza en buen estado." + "\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
                1, 3, Item.ItemType.Food));

            myItemList.Add(new Item("bananas", "Un plátano en buen estado." + "\n" + "Puede juntarse con otros ingredientes\n para crear mejores alimentos" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
                2, 3, Item.ItemType.Food));

            myItemList.Add(new Item("roastedFruits", "Frutas horneadas." + "\n" + "Un alimento muy nutritivo que restaurará\n más salud que un ingrediente común" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para comer, presiona la tecla 'U' \n mientras tienes el cursor encima del objeto" + "</color>",
                3, 10, Item.ItemType.Food));
            myItemList.Add(new Item("bottle", "Botella para utilizar de señuelo." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para equipar, presiona la tecla 'E' \n mientras tienes el cursor encima del objeto" + "</color>",
                4, 0, Item.ItemType.Quest));
            myItemList.Add(new Item("sneaker", "Zapatilla para usar como arma." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para equipar, presiona la tecla 'E' \n mientras tienes el cursor encima del objeto" + "</color>",
                5, 10, Item.ItemType.Weapon));
            myItemList.Add(new Item("rock", "Piedra para utilizar como señuelo." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para equipar, presiona la tecla 'E' \n mientras tienes el cursor encima del objeto" + "</color>",
                6, 0, Item.ItemType.Quest));
            myItemList.Add(new Item("cane", "Bastón para usar como arma." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " Para equipar, presiona la tecla 'E' \n mientras tienes el cursor encima del objeto" + "</color>",
                7, 20, Item.ItemType.Weapon));




        }
        else if (LanguageManager.idioma == 1)
        {


            myItemList.Add(new Item("apple", "An apple in good condition." + "\n" + " Can be combined with other ingredients \n to create better foods" +
                   "\n\n" + "\n\n<color=#fb8cff>" + " To eat, press the 'U' \n while holding the cursor on the object " + "</color>",
                   0, 3, Item.ItemType.Food));

            myItemList.Add(new Item("broccoli", "A vegetable in good condition." + "\n" + "Can be combined with other ingredients \n to create better foods" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To eat, press the 'U' \n while holding the cursor on the object" + "</color>",
                1, 3, Item.ItemType.Food));

            myItemList.Add(new Item("bananas", "A banana in good condition." + "\n" + "Can be combined with other ingredients \n to create better foods" +
                "\n\n" + "\n\n<color=#fb8cff>" + "To eat, press the 'U' \n while holding the cursor on the object" + "</color>",
                2, 3, Item.ItemType.Food));

            myItemList.Add(new Item("roastedFruits", "Baked fruits." + "\n" + "A very nutritious food that will restore \n more health than a common ingredient" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To eat, press the 'U' \n while holding the cursor on the object" + "</color>",
                3, 10, Item.ItemType.Food));
            myItemList.Add(new Item("bottle", "Bottle to use as a decoy." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To equip, press the 'E' \n while holding the cursor on the object" + "</color>",
                4, 0, Item.ItemType.Quest));
            myItemList.Add(new Item("sneaker", "Shoe to use as a weapon." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To equip, press the 'E' \n while holding the cursor on the object" + "</color>",
                5, 10, Item.ItemType.Weapon));
            myItemList.Add(new Item("rock", "Rock to use as a decoy." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To equip, press the 'E' \n while holding the cursor on the object" + "</color>",
                6, 0, Item.ItemType.Quest));
            myItemList.Add(new Item("cane", "Cane to use as a weapon." + "\n" +
                "\n\n" + "\n\n<color=#fb8cff>" + " To equip, press the 'E' \n while holding the cursor on the object" + "</color>",
                7, 20, Item.ItemType.Weapon));



        }



    }

}
