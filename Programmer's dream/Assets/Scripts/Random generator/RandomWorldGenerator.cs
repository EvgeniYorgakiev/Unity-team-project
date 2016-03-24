using System.Collections.Generic;
using UnityEngine;

public class RandomWorldGenerator : MonoBehaviour
{
    private const string PlatformTag = "Platform";
    private const string BugTag = "Bug";
    private const float SecondsUntilFirstBug = 10f;

    public List<Platform> platformsPrefabs = new List<Platform>();
    public List<Enemy> enemiesPrefabs = new List<Enemy>();
    private int numberOfObjectsInCollision = 0;
    private float timeSinceStart = 0.0f;
    private PlatformCreater platformCreater;
    private BugCreater bugCreater;

    private int NumberOfObjectsInCollision
    {
        get { return numberOfObjectsInCollision; }
        set { numberOfObjectsInCollision = value; }
    }

    private float TimeSinceStart
    {
        get { return timeSinceStart; }
        set { timeSinceStart = value; }
    }

    private PlatformCreater PlatformCreater
    {
        get { return platformCreater; }
        set { platformCreater = value; }
    }

    private BugCreater BugCreater
    {
        get { return bugCreater; }
        set { bugCreater = value; }
    }

    void Start()
    {
        this.PlatformCreater = new PlatformCreater(this.gameObject, this.platformsPrefabs);
        this.BugCreater = new BugCreater(this.gameObject, this.enemiesPrefabs);
    }

    void Update()
    {
        this.TimeSinceStart += Time.deltaTime;
    }

    void FixedUpdate ()
    {
        if (numberOfObjectsInCollision == 0)
        {
            this.PlatformCreater.CreatePlatform(this.transform.position, true, true);
            if (this.TimeSinceStart > SecondsUntilFirstBug)
            {
                this.BugCreater.CreateBug(this.transform.position, true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlatformTag)
        {
            this.NumberOfObjectsInCollision++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlatformTag)
        {
            this.NumberOfObjectsInCollision--;
        }
    }
}
