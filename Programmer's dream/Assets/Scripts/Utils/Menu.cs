﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameController gameController;
    public GameObject gameUI;
    public GameObject menuUI;
    public GameObject confirmationMenu;

    public void ActivateMenu()
    {
        this.gameUI.SetActive(false);
        this.menuUI.SetActive(true);
        gameController.gameIsRunning = false;
    }

    public void DeactivateMenu()
    {
        this.gameUI.SetActive(true);
        this.menuUI.SetActive(false);
        gameController.gameIsRunning = true;
    }

    public void ActivateConfirmationMenu()
    {
        this.menuUI.SetActive(false);
        this.confirmationMenu.SetActive(true);
    }

    public void DeactivateConfirmationMenu()
    {
        this.menuUI.SetActive(true);
        this.confirmationMenu.SetActive(false);
    }

    public void ActivateMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}