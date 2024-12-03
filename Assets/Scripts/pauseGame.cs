using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{

    [SerializeField] Canvas pauseMenu;

    private bool isPaused = false;
    private AudioSource[] musicSources;

    void Start()
    {
        // Automatically find all AudioSources in the scene
        musicSources = FindObjectsOfType<AudioSource>();
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f; // Pauses the game
        isPaused = true;
        Debug.Log("Game Paused");
        pauseMenu.gameObject.SetActive(true);
        foreach (AudioSource source in musicSources)
        {
            if (source != null && source.isPlaying)
            {
                source.Pause(); // Pause each music track
            }
        }


    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes the game
        isPaused = false;
        Debug.Log("Game Resumed");
        pauseMenu.gameObject.SetActive(false);
        foreach (AudioSource source in musicSources)
        {
            if (source != null)
            {
                source.UnPause(); // Resume each music track
            }
        }

    }

    //to main menu
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}


