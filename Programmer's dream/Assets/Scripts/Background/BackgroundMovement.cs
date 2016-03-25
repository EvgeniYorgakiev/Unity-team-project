using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private const string LeftMostPositionLayerName = "Left most position";

    public Transform leftMostPosition;
    public Transform rightMostPosition;
    public bool shouldBeDestroyed = false;
    public float speed = 2;
    private GameController gameController;
    private float distanceBetweenPositions;

    void Start()
    {
        if (this.leftMostPosition == null)
        {
            this.leftMostPosition = GameObject.FindGameObjectWithTag(LeftMostPositionLayerName).transform;
        }

        this.gameController = GameObject.FindGameObjectWithTag(Tags.GameControllerTag).GetComponent<GameController>();
        if (!this.shouldBeDestroyed)
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
            if (this.shouldBeDestroyed)
            {
                Destroy(this.gameObject);
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
}
