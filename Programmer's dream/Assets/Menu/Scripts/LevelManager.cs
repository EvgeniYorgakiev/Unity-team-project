﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform mainMenu;

    public Transform optionsMenu;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu(bool isClicked)
    {
        if (isClicked)
        {
            this.optionsMenu.gameObject.SetActive(isClicked);
            this.mainMenu.gameObject.SetActive(false);
        }
        else
        {
            this.optionsMenu.gameObject.SetActive(isClicked);
            this.mainMenu.gameObject.SetActive(true);
        }
    }
}