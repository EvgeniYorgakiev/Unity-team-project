using UnityEngine;

public class ItemManager : MonoBehaviour
{
    GameObject[] pool;
    
    void Start()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        int count = transform.childCount;
        pool = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            pool[i] = transform.GetChild(i).gameObject;
        }
    }

    public GameObject GetNextFreeItemFromPool(bool activate = true)
    {
        int count = pool.Length;
        for (int i = 0; i < count; i++)
        {
            if (!(pool[i].gameObject.GetComponent<SpriteRenderer>() != null && 
                pool[i].gameObject.GetComponent<SpriteRenderer>().enabled) || !pool[i].gameObject.activeSelf)
            {
                GameObject item = pool[i];
                if (activate)
                {
                    item.SetActive(true);
                    if (item.GetComponent<SpriteRenderer>() != null)
                    {
                        item.GetComponent<SpriteRenderer>().enabled = true;
                    }

                    if (item.GetComponent<BoxCollider2D>() != null)
                    {
                        item.GetComponent<BoxCollider2D>().enabled = true;
                    }

                    if (item.GetComponent<Platform>() != null)
                    {
                        item.GetComponent<Platform>().leftEdge.SetActive(true);
                        item.GetComponent<Platform>().rightEdge.SetActive(true);
                    }

                    foreach (Transform child in item.transform)
                    {
                        child.gameObject.SetActive(true);
                        if (child.GetComponent<SpriteRenderer>() != null)
                        {
                            child.GetComponent<SpriteRenderer>().enabled = true;
                        }

                        if (child.GetComponent<BoxCollider2D>() != null)
                        {
                            child.GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                }

                return item;
            }
        }

        Debug.Log("No free items in the pool");
        return null;
    }
}