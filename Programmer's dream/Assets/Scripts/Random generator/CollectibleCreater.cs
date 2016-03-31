using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleCreater
{
    private const float MaxPercentageForACollectible = 100f;

    private GameObject randomGenerator;
    public List<ItemManager> collectibles = new List<ItemManager>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<ItemManager> Collectibles
    {
        get { return collectibles; }
        set { collectibles = value; }
    }

    public CollectibleCreater(GameObject randomGenerator, List<ItemManager> collectibles)
    {
        this.RandomGenerator = randomGenerator;
        this.Collectibles = collectibles;
    }

    public void SpawnCollectible(GameObject randomObject)
    {
        for (int i = 0; i < this.collectibles.Count; i++)
        {
            try
            {
                Collectible randomCollectible =
                    this.Collectibles[i].GetNextFreeItemFromPool(false).GetComponent<Collectible>();
                float randomNumberForCollectible = Random.Range(0f, MaxPercentageForACollectible);
                if (randomNumberForCollectible < randomCollectible.chanceToSpawn)
                {
                    var newPosition = new Vector3(
                        randomObject.transform.position.x,
                        randomObject.transform.position.y + randomCollectible.YOffset,
                        this.collectibles[i].transform.position.z);

                    randomCollectible.transform.position = newPosition;
                    randomCollectible.gameObject.SetActive(true);

                    return;
                }
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}