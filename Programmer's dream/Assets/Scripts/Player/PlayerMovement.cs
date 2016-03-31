using System;
using UnityEngine;

public class PlayerMovement
{
    private const float VelocityTolerance = 0.00001f;
    private const float PlayerSpeed = 3f;

    private float jumpDistance;
    private GameObject gameObject;
    private GameController gameController;
    private bool fallingFast = false;
    private bool jumping = false;
    private float lastVelocity = 0;
    private AudioSource soundEffects;
    private AudioClip jump;
    private float speed;
    private float leftEdge;
    private float rightEdge;

    public PlayerMovement(GameObject gameObject, GameController gameController, float jumpDistance, AudioSource soundEffects, AudioClip jump, float leftEdge, float rightEdge)
    {
        this.GameObject = gameObject;
        this.GameController = gameController;
        this.JumpDistance = jumpDistance;
        this.SoundEffects = soundEffects;
        this.Jump = jump;
        this.Speed = PlayerSpeed;
        this.LeftEdge = leftEdge;
        this.RightEdge = rightEdge;
    }

    private float JumpDistance
    {
        get { return jumpDistance; }
        set { jumpDistance = value; }
    }

    private GameObject GameObject
    {
        get { return gameObject; }
        set { gameObject = value; }
    }

    private GameController GameController
    {
        get { return gameController; }
        set { gameController = value; }
    }

    private AudioSource SoundEffects
    {
        get { return soundEffects; }
        set { soundEffects = value; }
    }

    private AudioClip Jump
    {
        get { return jump; }
        set { jump = value; }
    }

    private bool FallingFast
    {
        get { return fallingFast; }
        set { fallingFast = value; }
    }

    private bool Jumping
    {
        get { return jumping; }
        set { jumping = value; }
    }

    private float LastVelocity
    {
        get { return lastVelocity; }
        set { lastVelocity = value; }
    }
    
    private float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private float LeftEdge
    {
        get { return leftEdge; }
        set { leftEdge = value; }
    }

    private float RightEdge
    {
        get { return rightEdge; }
        set { rightEdge = value; }
    }

    internal void FixedUpdate ()
    {
	    if (this.Jumping)
	    {
	        this.GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, this.JumpDistance));
            this.Jumping = false;
        }
        if (this.FallingFast)
        {
            this.GameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -this.JumpDistance));
            this.FallingFast = false;
        }
    }

    internal void Update()
    {
        bool isTouchingGround = Math.Abs(this.LastVelocity) < VelocityTolerance;
        bool isInTheAir = Math.Abs(this.LastVelocity) > VelocityTolerance;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            && isTouchingGround && this.GameController.gameIsRunning)
        {
            this.SoundEffects.clip = this.Jump;
            this.SoundEffects.Play();
            this.Jumping = true;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            && isInTheAir && this.GameController.gameIsRunning)
        {
            this.FallingFast = true;
        }
        bool isNotOnLeftEdge = this.GameObject.transform.position.x -
                               this.GameObject.GetComponent<BoxCollider2D>().size.x / 2 > this.LeftEdge;
        bool isNotOnRightEdge = this.GameObject.transform.position.x +
                                this.GameObject.GetComponent<BoxCollider2D>().size.x / 2 < this.RightEdge;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && this.GameController.gameIsRunning &&
            isNotOnLeftEdge && isTouchingGround)
        {
            float distance = Time.deltaTime * this.speed * gameController.globalSpeedModifier;
            this.GameObject.transform.position = new Vector3(
                this.GameObject.transform.position.x - distance,
                this.GameObject.transform.position.y,
                this.GameObject.transform.position.z);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && this.GameController.gameIsRunning &&
            isNotOnRightEdge)
        {
            float distance = Time.deltaTime*this.speed*gameController.globalSpeedModifier;
            if (isTouchingGround)
            {
                this.GameObject.transform.position = new Vector3(
                    this.GameObject.transform.position.x + distance,
                    this.GameObject.transform.position.y,
                    this.GameObject.transform.position.z);
            }
            else
            {
                this.GameObject.transform.position = new Vector3(
                    this.GameObject.transform.position.x + distance / 2,
                    this.GameObject.transform.position.y,
                    this.GameObject.transform.position.z);
            }
        }
        this.LastVelocity = this.gameObject.GetComponent<Rigidbody2D>().velocity.y;
    }
}
