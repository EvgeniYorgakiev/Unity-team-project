using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameController gameController;
    public GameObject gameUI;
    public Player player;
    public GameObject menuUI;
    public GameObject confirmationMenu;
    public GameObject restartMenu;
    private float animationSpeed;
    private bool isKinematic;

    public void ActivateMenu()
    {
        this.gameUI.SetActive(false);
        this.menuUI.SetActive(true);
        animationSpeed = player.animator.speed;
        player.animator.speed = 0;
        isKinematic = player.GetComponent<Rigidbody2D>().isKinematic;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        gameController.gameIsRunning = false;
    }

    public void DeactivateMenu()
    {
        this.gameUI.SetActive(true);
        this.menuUI.SetActive(false);
        if (player.PlayerKill.LivesLost <= PlayerKill.MaxLivesToLose)
        {
            player.animator.speed = animationSpeed;
            player.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
            gameController.gameIsRunning = true;
        }
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

    public void OpenRestartConfirmation()
    {
        this.menuUI.SetActive(false);
        this.restartMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeclineRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}