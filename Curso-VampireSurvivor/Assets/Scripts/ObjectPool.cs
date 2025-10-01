using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 32;
    [SerializeField] private bool canExpand = true;

    private readonly Queue <GameObject> pool = new Queue<GameObject>();
    private bool initialized;

    private void Awake()
    {
        Prewarm();
    }

    public void Prewarm()
    {
        if (initialized) return;
        initialized = true;

        for (int i = 0; i < initialSize; ++i)
        {
            CreateAndEnqueue();
        }
    }

    private GameObject CreateAndEnqueue()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);

        var poolable = go.GetComponent<Poolable>();
        if (poolable == null) poolable = go.AddComponent<Poolable>();
        poolable.SetOwnerPool(this);

        pool.Enqueue(go);
        return go;
    }

    public GameObject Get(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (pool.Count == 0)
        {
            if (!canExpand)
                return null;

            CreateAndEnqueue();
        }

        var go = pool.Dequeue();
        if (parent != null) go.transform.SetParent(parent, false);
        go.transform.SetPositionAndRotation(position, rotation);
        go.SetActive(true);

        return go;
    }

    public void Return (GameObject go)
    {
        if (go == null) return;
        go.SetActive(false);
        go.transform.SetParent(null, false);
        pool.Enqueue(go);
    }
}
