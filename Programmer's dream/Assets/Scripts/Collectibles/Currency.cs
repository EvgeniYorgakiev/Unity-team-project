using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private const string PossibleProfitText = "Possible Profit: ";
    private const int CurrencyValue = 100;
    private const float SpeedFactor = 5;

    public Sprite zeroSprite;
    private Text possibleProfitText;
    private bool beingAttracted = false;
    private GameObject magnetCollector;

    void Start()
    {
        this.possibleProfitText = GameObject.FindGameObjectWithTag(Tags.PossibleProfitTag).GetComponent<Text>();
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
            string currentScoreAsString = Regex.Match(this.possibleProfitText.text, "[0-9]+").Value;

            this.possibleProfitText.text = PossibleProfitText + (int.Parse(currentScoreAsString) + CurrencyValue).ToString();
            Destroy(this.gameObject);
        }
        else if (other.tag == Tags.MagnetCollectorTag && other.GetComponent<MagnetCollector>().enabled)
        {
            this.beingAttracted = true;
            this.magnetCollector = other.gameObject;
        }
    }
}