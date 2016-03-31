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
            if (!pool[i].gameObject.activeSelf)
            {
                GameObject item = pool[i];
                if (activate)
                {
                    item.gameObject.SetActive(true);
                    foreach (Transform child in item.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }

                return item;
            }
        }

        Debug.Log("No free items in the pool");
        return null;
    }
}