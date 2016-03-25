using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWorldGenerator : MonoBehaviour
{
    private const float SecondsUntilFirstBug = 10f;
    private const float MaxPercentageForACollectible = 100f;

    public List<Platform> platformsPrefabs = new List<Platform>();
    public List<Enemy> enemiesPrefabs = new List<Enemy>();
    public List<Collectible> collectibles = new List<Collectible>();
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
            int numberOfPossibleObjects = 1;
            if (this.TimeSinceStart > SecondsUntilFirstBug)
            {
                numberOfPossibleObjects++;
            }

            int randomObjectIndex = Random.Range(0, numberOfPossibleObjects);
            var randomObject = CreateRandomObject(randomObjectIndex);
            
            SpawnCollectible(randomObject);
        }
    }

    private void SpawnCollectible(GameObject randomObject)
    {
        for (int i = 0; i < this.collectibles.Count; i++)
        {
            float randomNumberForCollectible = Random.Range(0f, MaxPercentageForACollectible);
            if (randomNumberForCollectible < this.collectibles[i].chanceToSpawn)
            {
                var newPosition = new Vector3(
                    randomObject.transform.position.x,
                    randomObject.transform.position.y + this.collectibles[i].YOffset,
                    this.collectibles[i].transform.position.z);

                UnityEngine.Object.Instantiate(
                    this.collectibles[i].gameObject,
                    newPosition,
                    this.transform.rotation);

                return;
            }
        }
    }

    private GameObject CreateRandomObject(int randomObjectIndex)
    {
        switch (randomObjectIndex)
        {
            case 0:
            {
                return this.PlatformCreater.CreatePlatform(this.transform.position, true, true);
            }
            case 1:
            {
                return this.BugCreater.CreateBug(this.transform.position, true);
            }
            default:
            {
                throw new ArgumentException("Random number option not found");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.PlatformTag || collision.tag == Tags.BugTag)
        {
            this.NumberOfObjectsInCollision++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.PlatformTag || collision.tag == Tags.BugTag)
        {
            this.NumberOfObjectsInCollision--;
        }
    }
}
