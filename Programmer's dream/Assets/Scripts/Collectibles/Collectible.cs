using UnityEngine;

public class Collectible : BackgroundMovement
{
    public float chanceToSpawn;
    public float YOffset;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.PlayerTag)
        {
            this.Collect();
        }
    }

    public virtual void Collect()
    {
        this.gameObject.SetActive(false);
    }
}
