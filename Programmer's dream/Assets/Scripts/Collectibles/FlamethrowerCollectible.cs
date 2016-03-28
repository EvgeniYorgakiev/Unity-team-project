using UnityEngine;

public class FlamethrowerCollectible : Collectible
{
    public override void Collect()
    {
        var flamethrower = GameObject.FindGameObjectWithTag(Tags.PlayerTag).GetComponent<Player>().flamethrower;
        flamethrower.gameObject.SetActive(true);
        flamethrower.timeActive = 0.0f;

        base.Collect();
    }
}