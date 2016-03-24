using System.Collections.Generic;
using UnityEngine;

public class BugCreater
{
    private GameObject randomGenerator;
    private List<Enemy> enemiesPrefabs = new List<Enemy>();

    private GameObject RandomGenerator
    {
        get { return randomGenerator; }
        set { randomGenerator = value; }
    }

    private List<Enemy> EnemiesPrefabs
    {
        get { return enemiesPrefabs; }
        set { enemiesPrefabs = value; }
    }

    public BugCreater(GameObject randomGenerator, List<Enemy> enemiesPrefabs)
    {
        this.RandomGenerator = randomGenerator;
        this.EnemiesPrefabs = enemiesPrefabs;
    }

    public void CreateBug(Vector3 position, bool randomizeX)
    {
        int randomIndex = Random.Range(0, enemiesPrefabs.Count);
        float randomXOffset = 0;
        if (randomizeX)
        {
            float bugSize = enemiesPrefabs[randomIndex].GetComponent<BoxCollider2D>().size.x / 2;
            randomXOffset = Random.Range(
                -this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 2 + bugSize,
                 this.RandomGenerator.GetComponent<BoxCollider2D>().size.x / 2);
        }

        var newPosition = NewBugPosition(position, randomXOffset, randomIndex);

        Object.Instantiate(
            enemiesPrefabs[randomIndex].gameObject,
            newPosition,
            this.RandomGenerator.transform.rotation);
    }

    private Vector3 NewBugPosition(Vector3 position, float randomXOffset, int randomIndex)
    {
        Vector3 newPosition = new Vector3(
                position.x + randomXOffset,
                position.y + enemiesPrefabs[randomIndex].YOffset,
                position.z);

        return newPosition;
    }
}
