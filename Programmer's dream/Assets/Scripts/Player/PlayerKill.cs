using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

public class PlayerKill
{
    private const int MaxLivesToLose = 3;

    private int livesLost;
    private float startingPlayerXPosition;
    private GameObject highScore;
    private Text possibleProfit;
    private GameController gameController;
    private List<GameObject> lives;

    private GameController GameController
    {
        get { return this.gameController; }
        set { this.gameController = value; }
    }

    private int LivesLost
    {
        get { return this.livesLost; }
        set { this.livesLost = value; }
    }

    private float StartingPlayerXPosition
    {
        get { return this.startingPlayerXPosition; }
        set { this.startingPlayerXPosition = value; }
    }

    private GameObject HighScore
    {
        get { return this.highScore; }
        set { this.highScore = value; }
    }

    private Text PossibleProfit
    {
        get { return this.possibleProfit; }
        set { this.possibleProfit = value; }
    }

    private List<GameObject> Lives
    {
        get { return this.lives; }
        set { this.lives = value; }
    }

    public PlayerKill(GameController gameController, float startingPlayerXPosition, List<GameObject> lives, GameObject highScore, Text possibleProfit)
    {
        this.GameController = gameController;
        this.StartingPlayerXPosition = startingPlayerXPosition;
        this.HighScore = highScore;
        this.PossibleProfit = possibleProfit;
        this.LivesLost = 0;
        this.Lives = lives;
    }

    public void TakeLife(List<GameObject> objectsInCollision)
    {
        if (this.LivesLost == MaxLivesToLose)
        {
            KillPlayer();
        }
        else
        {
            this.Lives[this.LivesLost].SetActive(true);
            this.LivesLost++;
            SetCurrencyValue();
            for (int i = 0; i < objectsInCollision.Count; i++)
            {
                objectsInCollision[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetCurrencyValue()
    {
        int oldCurrencyValue = this.GameController.currencyValue;
        switch (this.LivesLost)
        {
            case 1:
            {
                this.GameController.currencyValue = 90;
                break;
            }
            case 2:
            {
                this.GameController.currencyValue = 75;
                break;
            }
            case 3:
            {
                this.GameController.currencyValue = 50;
                break;
            }
        }

        float difference = (float)this.GameController.currencyValue / oldCurrencyValue;
        int newPossibleProfitValue = (int)Math.Round(gameController.score * difference);
        this.PossibleProfit.text =
            Messages.PossibleProfitText + newPossibleProfitValue.ToString();
    }

    private void KillPlayer()
    {
        this.GameController.gameIsRunning = false;
    }
}
