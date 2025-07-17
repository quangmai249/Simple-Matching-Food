using UnityEngine;

public interface IPooledObject
{
    void ReturnToPool(GameObject obj, GameObject parent);
    GameObject OnSpawn(Vector3 pos, Quaternion rot);
}
