﻿using UnityEngine;

public class Magnet : Collectible
{
    public GameObject magnetCollector;

    public override void Collect()
    {
        magnetCollector.SetActive(true);
        magnetCollector.GetComponent<MagnetCollector>().timeActive = 0.0f;

        base.Collect();
    }
}
