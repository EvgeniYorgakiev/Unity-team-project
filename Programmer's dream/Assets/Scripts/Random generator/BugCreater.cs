using System.Collections.Generic;
using UnityEngine;

public class BugCreater
{
    private GameObject randomGenerator;
    private List<ItemManager> enemiesPrefabs = new List<ItemManager>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<ItemManager> EnemiesPrefabs
    {
        get { return enemiesPrefabs; }
        set { enemiesPrefabs = value; }
    }

    public BugCreater(GameObject randomGenerator, List<ItemManager> enemiesPrefabs)
    {
        this.RandomGenerator = randomGenerator;
        this.EnemiesPrefabs = enemiesPrefabs;
    }

    public GameObject CreateBug(Vector3 position, bool randomizeX)
    {
        int randomIndex = Random.Range(0, this.EnemiesPrefabs.Count);
        float randomXOffset = 0;
        Enemy randomEnemy = this.EnemiesPrefabs[randomIndex].GetNextFreeItemFromPool().GetComponent<Enemy>();
        if (randomizeX)
        {
            float bugSize = randomEnemy.GetComponent<BoxCollider2D>().size.x / 2;
            randomXOffset = Random.Range(
                -this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 4 + bugSize,
                 this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 2);
        }

        var newPosition = NewBugPosition(position, randomXOffset, randomEnemy);

        randomEnemy.transform.position = newPosition;

        return randomEnemy.gameObject;
    }

    private Vector3 NewBugPosition(Vector3 position, float randomXOffset, Enemy randomEnemy)
    {
        Vector3 newPosition = new Vector3(
                position.x + randomXOffset,
                position.y + randomEnemy.YOffset,
                position.z);

        return newPosition;
    }
}
