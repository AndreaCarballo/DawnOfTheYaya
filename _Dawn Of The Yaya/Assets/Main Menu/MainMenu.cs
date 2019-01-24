 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    private float musicVolume = 0.15f;
    public static bool load = false;

    public void PlayGame()
    {
        load = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME !");
        Application.Quit();

    }

    public void LoadGame()
    {
        load = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

    }

    public void click()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
