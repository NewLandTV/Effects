using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private List<GameObject> pool = new List<GameObject>();

    // Make and return add object to pool.
    public GameObject MakeInstance()
    {
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);

        pool.Add(instance);

        return instance;
    }

    // Make and add objects to pool by count.
    public void MakeInstance(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MakeInstance();
        }
    }

    // Return first object.
    public GameObject First() => pool[0];

    // Return disabled object. If all object is enabled return null.
    public GameObject Get()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }

        return null;
    }

    // Return pool by index to access.
    public GameObject Get(int index) => pool[index];

    /// Return last object.
    public GameObject Last() => pool[pool.Count - 1];

    // Remove all object and clear pool.
    public void DestroyAll()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject != null)
            {
                Destroy(pool[i].gameObject);
            }
        }

        pool.Clear();
    }
}
