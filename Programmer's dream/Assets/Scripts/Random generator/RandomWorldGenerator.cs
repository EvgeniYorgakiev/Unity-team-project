using System.Collections.Generic;
using UnityEngine;

public class RandomWorldGenerator : MonoBehaviour
{
    private const string PlatformTag = "Platform";
    private const float XDistanceBetweenPlatforms = 0.6f;
    private const float YDistanceBetweenPlatforms = 1.7f;

    public List<Platform> platformsPrefabs = new List<Platform>();
    private int numberOfPlatformsInCollision = 0;
	
	void FixedUpdate ()
    {
        if (numberOfPlatformsInCollision == 0)
        {
            CreatePlatform(this.transform.position, true, true);
        }
    }

    private void CreatePlatform(Vector3 position, bool randomizeX, bool startingPlatform)
    {
        int randomIndex = Random.Range(0, platformsPrefabs.Count);
        float randomXOffset = 0;
        if(randomizeX)
        {
            float platformSize = platformsPrefabs[randomIndex].GetComponent<BoxCollider2D>().size.x/2;
            randomXOffset = Random.Range(
                -this.GetComponent<BoxCollider2D>().size.x / 2 + platformSize,
                 this.GetComponent<BoxCollider2D>().size.x / 2);
        }

        var newPosition = NewPlatformPosition(position, startingPlatform, randomXOffset, randomIndex);

        GameObject platform = (GameObject)Object.Instantiate(
            platformsPrefabs[randomIndex].gameObject, 
            newPosition,
            this.transform.rotation);

        if (startingPlatform)
        {
            CreateBonusPlatforms(newPosition, platform.GetComponent<BoxCollider2D>().size.x / 2);
        }
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
            float platformSize = platformsPrefabs[randomIndex].GetComponent<BoxCollider2D>().size.x/2;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlatformTag)
        {
            numberOfPlatformsInCollision++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlatformTag)
        {
            numberOfPlatformsInCollision--;
        }
    }
}
