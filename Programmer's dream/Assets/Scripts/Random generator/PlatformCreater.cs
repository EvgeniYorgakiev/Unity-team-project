using System.Collections.Generic;
using UnityEngine;

public class PlatformCreater
{
    private const float XDistanceBetweenPlatforms = 1.5f;
    private const float YDistanceBetweenPlatforms = 1.7f;

    private GameObject randomGenerator;
    private List<ItemManager> platformsPrefabs = new List<ItemManager>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<ItemManager> PlatformsPrefabs
    {
        get { return platformsPrefabs; }
        set { platformsPrefabs = value; }
    }

    public PlatformCreater(GameObject randomGenerator, List<ItemManager> platformsPrefabs)
    {
        this.RandomGenerator = randomGenerator;
        this.PlatformsPrefabs = platformsPrefabs;
    }

    public GameObject CreatePlatform(Vector3 position, bool randomizeX, bool startingPlatform)
    {
        int randomIndex = Random.Range(0, this.PlatformsPrefabs.Count);
        float randomXOffset = 0;
        Platform randomPlatform = this.PlatformsPrefabs[randomIndex].GetNextFreeItemFromPool().GetComponent<Platform>();
        if (randomizeX)
        {
            float platformSize = randomPlatform.GetComponent<BoxCollider2D>().size.x / 2;
            randomXOffset = Random.Range(
                -this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 4 + platformSize,
                 this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 2);
        }

        var newPosition = NewPlatformPosition(position, startingPlatform, randomXOffset, randomPlatform);

        randomPlatform.transform.position = newPosition;

        if (startingPlatform)
        {
            CreateBonusPlatforms(newPosition, randomPlatform.GetComponent<BoxCollider2D>().size.x / 2);
        }

        return randomPlatform.gameObject;
    }

    private Vector3 NewPlatformPosition(Vector3 position, bool startingPlatform, float randomXOffset, Platform randomPlatform)
    {
        Vector3 newPosition;
        if (startingPlatform)
        {
            newPosition = new Vector3(
                position.x + randomXOffset,
                position.y + randomPlatform.YOffset,
                position.z);
        }
        else
        {
            float platformSize = randomPlatform.GetComponent<BoxCollider2D>().size.x / 2;
            newPosition = new Vector3(
                position.x + randomXOffset + platformSize + XDistanceBetweenPlatforms,
                position.y,
                position.z);
        }
        return newPosition;
    }

    private void CreateBonusPlatforms(Vector3 position, float sizeOfPlatform)
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            Vector3 newPosition = new Vector3(
                position.x + sizeOfPlatform,
                position.y + YDistanceBetweenPlatforms,
                position.z);
            CreatePlatform(newPosition, false, false);
        }
    }

}
