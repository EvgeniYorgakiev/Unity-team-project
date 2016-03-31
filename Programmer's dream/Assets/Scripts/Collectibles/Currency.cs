using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private const float SpeedFactor = 5;

    public Sprite zeroSprite;
    public Text possibleProfitText;
    public GameObject magnetCollector;
    public HighScore highScore;
    public GameController gameController;
    private bool beingAttracted = false;

    void OnEnable()
    {
        int textValue = Random.Range(0, 2);
        if (textValue == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = zeroSprite;
        }
    }

    void Update()
    {
        if (this.beingAttracted)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                magnetCollector.transform.position,
                Time.deltaTime * SpeedFactor);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.PlayerTag)
        {
            gameController.score += gameController.currencyValue;

            this.possibleProfitText.text = Messages.PossibleProfitText + gameController.score;
            
            if (gameController.score > highScore.highScore)
            {
                this.highScore.GetComponent<Text>().text = Messages.HighScoreDescription + gameController.score.ToString();
                this.highScore.GetComponent<HighScore>().Save();
            }

            this.gameObject.SetActive(false);
        }
        else if (other.tag == Tags.MagnetCollectorTag && other.GetComponent<MagnetCollector>().enabled)
        {
            this.beingAttracted = true;
            this.magnetCollector = other.gameObject;
        }
    }
}