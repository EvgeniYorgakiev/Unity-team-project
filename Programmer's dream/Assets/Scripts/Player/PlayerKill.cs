using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class PlayerKill
{
    private const string HighScoreDescription = "High score: ";

    private float startingPlayerXPosition;
    private GameObject highScore;
    private Text possibleProfit;
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

    private GameObject HighScore
    {
        get { return highScore; }
        set { highScore = value; }
    }

    private Text PossibleProfit
    {
        get { return possibleProfit; }
        set { possibleProfit = value; }
    }

    public PlayerKill(GameController gameController, float startingPlayerXPosition)
    {
        this.GameController = gameController;
        this.StartingPlayerXPosition = startingPlayerXPosition;
        this.HighScore = GameObject.FindGameObjectWithTag(Tags.HighScoreTag).gameObject;
        this.PossibleProfit = GameObject.FindGameObjectWithTag(Tags.PossibleProfitTag).GetComponent<Text>();
    }

    public void Update(float currentPlayerXPosition)
    {
        if (currentPlayerXPosition < this.StartingPlayerXPosition)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
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
