using UnityEngine;
using System.Collections;

public class PlayerKill
{
    private float startingPlayerXPosition;
    private GameController gameController;

    private GameController GameController
    {
        get { return gameController; }
        set { gameController = value; }
    }

    private float StartingPlayerXPosition
    {
        get { return startingPlayerXPosition; }
        set { startingPlayerXPosition = value; }
    }

    public PlayerKill(GameController gameController, float startingPlayerXPosition)
    {
        this.GameController = gameController;
        this.StartingPlayerXPosition = startingPlayerXPosition;
    }

    public void Update(float currentPlayerXPosition)
    {
        if (currentPlayerXPosition < this.StartingPlayerXPosition)
        {
            this.GameController.gameIsRunning = false;
        }
    }
}
