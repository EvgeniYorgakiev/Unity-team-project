﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float DistanceFromLeftEdge = 1f;

    public float jumpDistance;
    public GameController gameController;
    public List<GameObject> lives;
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

        this.PlayerMovement = new PlayerMovement(this.gameObject, this.gameController, this.jumpDistance);

        this.PlayerKill = new PlayerKill(this.gameController, this.gameObject.transform.position.x, this.lives, this.gameObject);
    }

    void FixedUpdate()
    {
        this.PlayerMovement.FixedUpdate();
    }

    void Update ()
    {
        this.PlayerMovement.Update();

        this.PlayerKill.Update(this.gameObject.transform.position.x, this.objectsInCollisionRange);
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Tags.BugTag || other.tag == Tags.PlatformTag)
        {
            this.objectsInCollisionRange.Remove(other.gameObject);
        }
    }
}
