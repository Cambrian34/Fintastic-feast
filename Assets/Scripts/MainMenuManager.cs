using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //change scene to main game
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    //change scene to options menu
    public void Options()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }

    //quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //to main menu
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
