using System.Collections.Generic;
using UnityEngine;

public class PlatformCreater
{
    private const float XDistanceBetweenPlatforms = 0.7f;
    private const float YDistanceBetweenPlatforms = 1.7f;

    private GameObject randomGenerator;
    private List<Platform> platformsPrefabs = new List<Platform>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<Platform> PlatformsPrefabs
    {
        get { return platformsPrefabs; }
        set { platformsPrefabs = value; }
    }

    public PlatformCreater(GameObject randomGenerator, List<Platform> platformsPrefabs)
    {
        this.RandomGenerator = randomGenerator;
        this.PlatformsPrefabs = platformsPrefabs;
    }

    public GameObject CreatePlatform(Vector3 position, bool randomizeX, bool startingPlatform)
    {
        int randomIndex = UnityEngine.Random.Range(0, platformsPrefabs.Count);
        float randomXOffset = 0;
        if (randomizeX)
        {
            float platformSize = platformsPrefabs[randomIndex].GetComponent<BoxCollider2D>().size.x / 2;
            randomXOffset = UnityEngine.Random.Range(
                -this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 4 + platformSize,
                 this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 2);
        }

        var newPosition = NewPlatformPosition(position, startingPlatform, randomXOffset, randomIndex);

        GameObject platform = (GameObject)UnityEngine.Object.Instantiate(
            platformsPrefabs[randomIndex].gameObject,
            newPosition,
            this.RandomGenerator.transform.rotation);

        if (startingPlatform)
        {
            CreateBonusPlatforms(newPosition, platform.GetComponent<BoxCollider2D>().size.x / 2);
        }

        return platform;
    }

    private Vector3 NewPlatformPosition(Vector3 position, bool startingPlatform, float randomXOffset, int randomIndex)
    {
        Vector3 newPosition;
        if (startingPlatform)
        {
            newPosition = new Vector3(
                position.x + randomXOffset,
                position.y + platformsPrefabs[randomIndex].YOffset,
                position.z);
        }
        else
        {
            float platformSize = platformsPrefabs[randomIndex].GetComponent<BoxCollider2D>().size.x / 2;
            newPosition = new Vector3(
                position.x + randomXOffset + platformSize + XDistanceBetweenPlatforms,
                position.y,
                position.z);
        }
        return newPosition;
    }

    private void CreateBonusPlatforms(Vector3 position, float sizeOfPlatform)
    {
        int randomNumber = UnityEngine.Random.Range(0, 2);
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
