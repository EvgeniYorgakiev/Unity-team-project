using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Options : MonoBehaviour
{
    public Toggle toggle;
    public SoundsScript sounds;

    void Start()
    {
        this.Load();
    }

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/options.gd");
            bf.Serialize(file, toggle.isOn.ToString());
            file.Close();
        }
        catch (IOException)
        {
        }
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/options.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.gd", FileMode.Open);
            toggle.isOn = bool.Parse(bf.Deserialize(file).ToString());
            file.Close();
        }
    }
}
