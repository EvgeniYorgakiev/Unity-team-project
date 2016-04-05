using UnityEngine;

public class Enemy : BackgroundMovement
{
    public float YOffset;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.FlamethrowerTag)
        {
            this.HideObject();
        }
    }
}
