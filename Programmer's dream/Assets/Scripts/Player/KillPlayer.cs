using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private const string PlatformTag = "Platform";

    public GameController gameController; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlatformTag)
        {
            gameController.gameIsRunning = false;
        }
    }
}
