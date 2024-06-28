using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject characterScreen;

    void Start()
    {
        // if (PlayerStats.instance != null)
        // {
        //     PlayerStats.instance.DestroySingleton();
        // }

        titleScreen.SetActive(true);
        characterScreen.SetActive(false);
    }

    public void CharacterSelect()
    {
        titleScreen.SetActive(false);
        characterScreen.SetActive(true);
    }

    public void LevelSelect(string levelName)
    {
        SceneController.instance.LoadGameplay(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
