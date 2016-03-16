using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private const string CollectorTag = "Collector";
    private const string PossibleProfitTag = "Possible profit";
    private const string PossibleProfitText = "Possible Profit: ";
    private const int CurrencyValue = 100;

    private Text possibleProfitText;

    void Start()
    {
        this.possibleProfitText = GameObject.FindGameObjectWithTag(PossibleProfitTag).GetComponent<Text>();
        int textValue = Random.Range(0, 2);
        this.GetComponent<TextMesh>().text = textValue.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == CollectorTag)
        {
            string valueAsString = Regex.Match(this.possibleProfitText.text, "[0-9]+").Value;

            this.possibleProfitText.text = PossibleProfitText + (int.Parse(valueAsString) + CurrencyValue).ToString();
            Destroy(this.gameObject);
        }
    }
}
