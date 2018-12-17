using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedData
{

    public int difficulty;
    public int zone;
    public float time;
    public int lifeLost;
    public int language;
    public List<SavedZombie> zombies;
    public SavePlayer player;

}
