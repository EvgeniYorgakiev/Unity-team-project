﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWorldGenerator : MonoBehaviour
{
    private const float SecondsUntilFirstBug = 30f;

    public List<ItemManager> platformsPrefabs = new List<ItemManager>();
    public List<ItemManager> enemiesPrefabs = new List<ItemManager>();
    public List<ItemManager> collectibles = new List<ItemManager>();
    private int numberOfObjectsInCollision = 0;
    private float timeSinceStart = 0.0f;
    private PlatformCreater platformCreater;
    private BugCreater bugCreater;
    private CollectibleCreater collectibleCreater;

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

    private CollectibleCreater CollectibleCreater
    {
        get { return collectibleCreater; }
        set { collectibleCreater = value; }
    }

    void Start()
    {
        this.PlatformCreater = new PlatformCreater(this.gameObject, this.platformsPrefabs);
        this.BugCreater = new BugCreater(this.gameObject, this.enemiesPrefabs);
        this.CollectibleCreater = new CollectibleCreater(this.gameObject, this.collectibles);
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

            if (randomObject != null)
            {
                CollectibleCreater.SpawnCollectible(randomObject);
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
