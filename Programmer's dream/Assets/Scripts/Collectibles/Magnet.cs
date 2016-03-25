using UnityEngine;

public class Magnet : Collectible
{
    public override void Collect()
    {
        var magnetCollector = GameObject.FindGameObjectWithTag(Tags.MagnetCollectorTag);
        magnetCollector.GetComponent<MagnetCollector>().enabled = true;
        magnetCollector.GetComponent<MagnetCollector>().timeActive = 0.0f;

        base.Collect();
    }
}
