using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string PossibleProfitTag = "Possible profit";
    private const string PossibleProfitText = "Possible Profit: ";
    private const int CurrencyValue = 100;

    public Sprite zeroSprite;
    private Text possibleProfitText;

    void Start()
    {
        this.possibleProfitText = GameObject.FindGameObjectWithTag(PossibleProfitTag).GetComponent<Text>();
        int textValue = Random.Range(0, 2);
        if (textValue == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = zeroSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlayerTag)
        {
            string currentScoreAsString = Regex.Match(this.possibleProfitText.text, "[0-9]+").Value;

            this.possibleProfitText.text = PossibleProfitText + (int.Parse(currentScoreAsString) + CurrencyValue).ToString();
            Destroy(this.gameObject);
        }
    }
}
