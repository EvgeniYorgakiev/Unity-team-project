using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

public class PlayerKill
{
    private const string HighScoreDescription = "High score: ";
    private const int MaxLivesToLose = 3;

    private int livesLost;
    private float startingPlayerXPosition;
    private GameObject highScore;
    private Text possibleProfit;
    private GameController gameController;
    private List<GameObject> lives;
    private GameObject player;

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

    private GameObject Player
    {
        get { return this.player; }
        set { this.player = value; }
    }

    public PlayerKill(GameController gameController, float startingPlayerXPosition, List<GameObject> lives, GameObject player)
    {
        this.GameController = gameController;
        this.StartingPlayerXPosition = startingPlayerXPosition;
        this.HighScore = GameObject.FindGameObjectWithTag(Tags.HighScoreTag).gameObject;
        this.PossibleProfit = GameObject.FindGameObjectWithTag(Tags.PossibleProfitTag).GetComponent<Text>();
        this.LivesLost = 0;
        this.Lives = lives;
        this.Player = player;
    }

    public void Update(float currentPlayerXPosition, List<GameObject> objectsInCollision)
    {
        if (currentPlayerXPosition < this.StartingPlayerXPosition)
        {
            TakeLife(objectsInCollision);
        }
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
            this.Player.transform.position = new Vector3(
                this.StartingPlayerXPosition, 
                player.transform.position.y,
                player.transform.position.z);
            for (int i = 0; i < objectsInCollision.Count; i++)
            {
                UnityEngine.Object.Destroy(objectsInCollision[i]);
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
        string currentScoreAsString = Regex.Match(this.PossibleProfit.text, "[0-9]+").Value;

        float difference = (float)this.GameController.currencyValue / oldCurrencyValue;
        int newPossibleProfitValue = (int)Math.Round(int.Parse(currentScoreAsString) * difference);
        this.PossibleProfit.text =
            Messages.PossibleProfitText + newPossibleProfitValue.ToString();
    }

    private void KillPlayer()
    {
        this.GameController.gameIsRunning = false;
        int currentScore = int.Parse(Regex.Match(this.PossibleProfit.text, "[0-9]+").Value);
        int highScoreValue = int.Parse(Regex.Match(this.HighScore.GetComponent<Text>().text, "[0-9]+").Value);

        if (currentScore > highScoreValue)
        {
            this.HighScore.GetComponent<Text>().text = HighScoreDescription + (currentScore).ToString();
            this.HighScore.GetComponent<HighScore>().Save();
        }
    }
}
