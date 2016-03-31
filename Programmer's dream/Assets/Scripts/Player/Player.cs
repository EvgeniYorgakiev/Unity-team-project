using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const float DistanceFromLeftEdge = 1f;

    public float jumpDistance;
    public GameController gameController;
    public Flamethrower flamethrower;
    public List<GameObject> lives;
    public AudioSource soundEffect;
    public AudioClip jump;
    public GameObject highscore;
    public Text possibleProfit;
    private PlayerMovement playerMovement;
    private PlayerKill playerKill;
    private List<GameObject> objectsInCollisionRange = new List<GameObject>();

    public PlayerMovement PlayerMovement
    {
        get { return playerMovement; }
        set { playerMovement = value; }
    }

    public PlayerKill PlayerKill
    {
        get { return playerKill; }
        set { playerKill = value; }
    }

    void Start ()
    {
        var cameraPos = Camera.main.transform.position;
        var width = Camera.main.orthographicSize * Screen.width / Screen.height;
        this.gameObject.transform.position = new Vector3(
            cameraPos.x - width + DistanceFromLeftEdge,
            this.gameObject.transform.position.y,
            this.gameObject.transform.position.z);

        this.PlayerMovement = new PlayerMovement(
            this.gameObject, 
            this.gameController,
            this.jumpDistance, 
            this.soundEffect, 
            this.jump,
            cameraPos.x - width,
            cameraPos.x + width);

        this.PlayerKill = new PlayerKill(this.gameController, this.gameObject.transform.position.x, this.lives, highscore, possibleProfit);
    }

    void FixedUpdate()
    {
        this.PlayerMovement.FixedUpdate();
    }

    void Update ()
    {
        if (gameController.gameIsRunning)
        {
            this.PlayerMovement.Update();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.BugTag)
        {
            this.objectsInCollisionRange.Add(other.gameObject);
            PlayerKill.TakeLife(this.objectsInCollisionRange);
        }
        else if (other.tag == Tags.PlatformTag)
        {
            this.objectsInCollisionRange.Add(other.gameObject);
        }
        else if (other.tag == Tags.EdgeTag)
        {
            this.objectsInCollisionRange.Add(other.gameObject.transform.parent.gameObject);
            PlayerKill.TakeLife(this.objectsInCollisionRange);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Tags.BugTag || other.tag == Tags.PlatformTag)
        {
            this.objectsInCollisionRange.Remove(other.gameObject);
        }
    }
}
