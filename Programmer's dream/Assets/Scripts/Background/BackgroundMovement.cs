using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private const string LeftMostPositionLayerName = "Left most position";

    public Transform leftMostPosition;
    public Transform rightMostPosition;
    public bool shouldBeDestroyed = false;
    public float speed = 2;

    void Start()
    {
        if (this.leftMostPosition == null)
        {
            this.leftMostPosition = GameObject.FindGameObjectWithTag(LeftMostPositionLayerName).transform;
        }
    }

    public virtual void Update ()
    {
        if (this.transform.position.x < this.leftMostPosition.position.x)
        {
            if (this.shouldBeDestroyed)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.transform.position = new Vector3(
                    this.rightMostPosition.position.x,
                    this.transform.position.y,
                    this.transform.position.z);
            }
        }
        else
        {
            this.transform.position = new Vector3(
                    this.transform.position.x - Time.deltaTime * this.speed,
                    this.transform.position.y,
                    this.transform.position.z);
        }
	}
}
