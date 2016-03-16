using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpDistance;
    public GameController gameController;
    private bool grounded = true;
	
	void FixedUpdate ()
    {
	    if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && gameController.gameIsRunning)
	    {
	        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpDistance));
            grounded = false;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.position.y < this.transform.position.y)
        {
            grounded = true;
        }
    }
}
