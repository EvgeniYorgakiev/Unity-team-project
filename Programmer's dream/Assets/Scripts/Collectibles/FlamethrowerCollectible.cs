using UnityEngine;

public class FlamethrowerCollectible : Collectible
{
    public Flamethrower flamethrower;

    public override void Collect()
    {
        flamethrower.gameObject.SetActive(true);
        flamethrower.timeActive = 0.0f;

        base.Collect();
    }
}