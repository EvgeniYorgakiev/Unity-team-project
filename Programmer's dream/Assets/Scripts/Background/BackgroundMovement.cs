using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private const string LeftMostPositionLayerName = "Left most position";

    public Transform leftMostPosition;
    public Transform rightMostPosition;
    public bool shouldBePortedToRight = false;
    public float speed = 2;
    public GameController gameController;
    private float distanceBetweenPositions;

    void Start()
    {
        if (shouldBePortedToRight)
        {
            this.distanceBetweenPositions = this.rightMostPosition.position.x - this.leftMostPosition.position.x;
        }
    }

    public virtual void Update ()
    {
        if (!this.gameController.gameIsRunning)
        {
            return;
        }
        if (this.transform.position.x < this.leftMostPosition.position.x)
        {
            if (!shouldBePortedToRight)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.transform.position = new Vector3(
                    this.transform.position.x + this.distanceBetweenPositions,
                    this.transform.position.y,
                    this.transform.position.z);
            }
        }

        this.transform.position = new Vector3(
                        this.transform.position.x - Time.deltaTime * this.speed * gameController.globalSpeedModifier,
                        this.transform.position.y,
                        this.transform.position.z);
	}

    public void HideObject()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        if (this.GetComponent<Platform>() != null)
        {
            this.GetComponent<Platform>().leftEdge.SetActive(false);
            this.GetComponent<Platform>().rightEdge.SetActive(false);
        }
    }
}
