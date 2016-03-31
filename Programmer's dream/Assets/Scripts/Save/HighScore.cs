using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    internal int highScore = 0;

    void Start()
    {
        this.Load();
    }

	public void Save ()
    {
	    try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
            bf.Serialize(file, this.GetComponent<Text>().text);
            file.Close();
        }
	    catch (IOException)
	    {
	    }
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            this.GetComponent<Text>().text = bf.Deserialize(file).ToString();
            highScore = int.Parse(Regex.Match(this.GetComponent<Text>().text, "[0-9]+").Value);
            file.Close();
        }
    }
}
