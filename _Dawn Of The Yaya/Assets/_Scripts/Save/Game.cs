using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class Game : MonoBehaviour
{

    public int difficulty = 1;
    public int language;
    ManageDifficulty manDif;
    PlayerHealth playerHealth;
    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manDif = GameObject.FindGameObjectWithTag("Player").GetComponent<ManageDifficulty>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        manDif.buildObjectList();
        manDif.objectsSet();
        if (MainMenu.load)
        {
            Load();
            MainMenu.load = false;
        }
    }

    public void Save()
    {

        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat"); //the creates a file in the unity app data path
        SavedData game = new SavedData();
        Tutorial tuto = player.GetComponent<Tutorial>();
        CamaraControl cam = player.GetComponent<CamaraControl>();
        game.tutorial = tuto.EndTuto;
        game.cinematic = cam.CinematicDone;
        game.difficulty = difficulty;
        game.zone = manDif.zone;
        game.time = manDif.time;
        game.lifeLost = playerHealth.lifeLost;
        game.language = LanguageManager.idioma;
        game.player = new SavePlayer();
        game.player.posX = player.transform.position.x;
        game.player.posY = player.transform.position.y;
        game.player.posZ = player.transform.position.z;
        game.player.rX = player.transform.rotation.x;
        game.player.rY = player.transform.rotation.y;
        game.player.rZ = player.transform.rotation.z;
        game.player.rW = player.transform.rotation.w;
        game.player.health = playerHealth.currentHealth;
        game.zombies = new List<SavedZombie>();
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ZombieAgent agent = zombie.GetComponent<ZombieAgent>();
            EnemyAttack attack = zombie.GetComponent<EnemyAttack>();
            EnemyHealth health = zombie.GetComponent<EnemyHealth>();
            SavedZombie zom = new SavedZombie();
            zom.id = agent.ID;
            zom.posX = zombie.transform.position.x;
            zom.posY = zombie.transform.position.y;
            zom.posZ = zombie.transform.position.z;
            zom.rX = zombie.transform.rotation.x;
            zom.rY = zombie.transform.rotation.y;
            zom.rZ = zombie.transform.rotation.z;
            zom.rW = zombie.transform.rotation.w;
            zom.speed = agent.walkingSpeed;
            zom.health = health.currentHealth;
            zom.damage = attack.attackDamage;
            game.zombies.Add(zom);

        }
        binary.Serialize(file, game);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveGame.dat"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.Open);
            SavedData game = (SavedData)binary.Deserialize(file);
            file.Close();
            Tutorial tuto = player.GetComponent<Tutorial>();
            CamaraControl cam = player.GetComponent<CamaraControl>();
            tuto.EndTuto = game.tutorial;
            cam.CinematicDone = game.cinematic;
            difficulty = game.difficulty;
            manDif.zone = game.zone;
            manDif.time = game.time;
            playerHealth.lifeLost = game.lifeLost;
            language = game.language;
            player.transform.position = new Vector3(game.player.posX, game.player.posY, game.player.posZ);
            player.transform.rotation = new Quaternion(game.player.rX, game.player.rY, game.player.rZ, game.player.rZ);
            playerHealth.currentHealth = game.player.health;
            foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                ZombieAgent agent = zombie.GetComponent<ZombieAgent>();
                EnemyAttack attack = zombie.GetComponent<EnemyAttack>();
                EnemyHealth health = zombie.GetComponent<EnemyHealth>();
                foreach (SavedZombie saved in game.zombies)
                {
                    if (saved.id == agent.ID)
                    {
                        Vector3 pos = new Vector3(saved.posX, saved.posY, saved.posZ);
                        zombie.transform.position = pos;
                        zombie.transform.rotation = new Quaternion(saved.rX, saved.rY, saved.rZ, saved.rW);
                        agent.walkingSpeed = saved.speed;
                        health.currentHealth = saved.health;
                        attack.attackDamage = saved.damage;
                        break;
                    }
                }

            }
        }
    }

    public void setDifficultyToEasy()
    {

        difficulty = 0;
        diffChanges();
    }

    public void setDifficultyToNormal()
    {

        difficulty = 1;
        diffChanges();
    }

    public void setDifficultyToHard()
    {

        difficulty = 2;
        diffChanges();
    }

    public void diffChanges()
    {
        manDif.objectsSet();
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ZombieAgent agent = zombie.GetComponent<ZombieAgent>();
            agent.applyDifficultySettings();

        }
    }
}
