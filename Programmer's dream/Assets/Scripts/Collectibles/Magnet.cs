using UnityEngine;

public class Magnet : Collectible
{
    public GameObject magnetCollector;

    public override void Collect()
    {
        magnetCollector.GetComponent<MagnetCollector>().enabled = true;
        magnetCollector.GetComponent<MagnetCollector>().timeActive = 0.0f;

        base.Collect();
    }
}
