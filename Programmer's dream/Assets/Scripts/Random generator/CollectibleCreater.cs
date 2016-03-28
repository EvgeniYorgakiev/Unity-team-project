using System.Collections.Generic;
using UnityEngine;

public class CollectibleCreater
{
    private const float MaxPercentageForACollectible = 100f;

    private GameObject randomGenerator;
    public List<Collectible> collectibles = new List<Collectible>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<Collectible> Collectibles
    {
        get { return collectibles; }
        set { collectibles = value; }
    }

    public CollectibleCreater(GameObject randomGenerator, List<Collectible> collectibles)
    {
        this.RandomGenerator = randomGenerator;
        this.Collectibles = collectibles;
    }

    public void SpawnCollectible(GameObject randomObject)
    {
        for (int i = 0; i < this.collectibles.Count; i++)
        {
            float randomNumberForCollectible = Random.Range(0f, MaxPercentageForACollectible);
            if (randomNumberForCollectible < this.Collectibles[i].chanceToSpawn)
            {
                var newPosition = new Vector3(
                    randomObject.transform.position.x,
                    randomObject.transform.position.y + this.collectibles[i].YOffset,
                    this.collectibles[i].transform.position.z);

                UnityEngine.Object.Instantiate(
                    this.collectibles[i].gameObject,
                    newPosition,
                    randomGenerator.transform.rotation);

                return;
            }
        }
    }
}