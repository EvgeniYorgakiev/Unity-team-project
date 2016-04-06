using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const float DistanceFromLeftEdge = 1f;
    private const float YSizeOfPlatform = 0.72f;

    public float jumpDistance;
    public GameController gameController;
    public Menu menu;
    public Flamethrower flamethrower;
    public List<GameObject> lives;
    public AudioSource soundEffect;
    public AudioClip jump;
    public GameObject highscore;
    public Text possibleProfit;
    public Animator animator;
    private PlayerMovement playerMovement;
    private PlayerKill playerKill;
    private readonly List<GameObject> objectsInCollisionRange = new List<GameObject>();
    private int numberOfColliders = 0;
    private string tagOfObjectBelowBottomRightCorner = string.Empty;
    private string tagOfObjectBelowBottomLeftCorner = string.Empty;

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

        this.PlayerKill = new PlayerKill(this.gameController, this.menu, this.gameObject.transform.position.x, this.lives, highscore, possibleProfit);
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
            Vector2 bottomRightCorner = new Vector2(
                this.transform.position.x + this.gameObject.GetComponent<BoxCollider2D>().size.x / 2,
                this.transform.position.y - this.gameObject.GetComponent<BoxCollider2D>().size.y / 2 - YSizeOfPlatform);
            Vector2 bottomLeftCorner = new Vector2(
                this.transform.position.x - this.gameObject.GetComponent<BoxCollider2D>().size.x / 2,
                this.transform.position.y - this.gameObject.GetComponent<BoxCollider2D>().size.y / 2 - YSizeOfPlatform);

            var rayFromBottomRightCorner = Physics2D.Raycast(bottomRightCorner, Vector2.down);
            var rayFromBottomLeftCorner = Physics2D.Raycast(bottomLeftCorner, Vector2.down);

            if (rayFromBottomRightCorner.collider != null)
            {
                tagOfObjectBelowBottomRightCorner = rayFromBottomRightCorner.collider.tag;
            }

            if (rayFromBottomLeftCorner.collider != null)
            {
                tagOfObjectBelowBottomLeftCorner = rayFromBottomLeftCorner.collider.tag;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.BugTag)
        {
            this.objectsInCollisionRange.Add(other.gameObject);
            PlayerKill.TakeLife(this.objectsInCollisionRange, this.animator);
        }
        else if (other.tag == Tags.PlatformTag)
        {
            this.objectsInCollisionRange.Add(other.gameObject);
        }
        else if (other.tag == Tags.EdgeTag)
        {
            if (tagOfObjectBelowBottomLeftCorner == Tags.PlatformTag ||
                tagOfObjectBelowBottomRightCorner == Tags.PlatformTag)
            {
                return;
            }

            this.objectsInCollisionRange.Add(other.gameObject.transform.parent.gameObject);
            PlayerKill.TakeLife(this.objectsInCollisionRange, this.animator);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Tags.BugTag || other.tag == Tags.PlatformTag)
        {
            this.objectsInCollisionRange.Remove(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (numberOfColliders == 0)
        {
            this.animator.SetBool("Grounded", true);
            this.animator.Play("Run");
        }
        this.numberOfColliders++;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        this.numberOfColliders--;
        if (numberOfColliders == 0)
        {
            this.animator.SetBool("Grounded", false);
            this.animator.Play("Jump");
        }
    }
}
