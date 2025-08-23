using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPooledObject
{
    private GameObject _prefab;
    private Queue<GameObject> _pool = new Queue<GameObject>();
    public void ReturnToPool(GameObject obj, GameObject parent)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent.transform);
        _pool.Enqueue(obj.gameObject);
    }
    public GameObject OnSpawn(Vector3 pos, Quaternion rot)
    {
        GameObject obj = _pool.Count > 0 ? _pool.Dequeue() : GameObject.Instantiate(_prefab);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        return obj;
    }
    public ObjectPool(GameObject prefab, Transform parent, int initialSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(parent.transform);
            _pool.Enqueue(obj);
        }
    }
    public int PoolSize
    {
        get => _pool.Count;
    }
    public Queue<GameObject> Pool
    {
        set => _pool = value;
        get => _pool;
    }
}
