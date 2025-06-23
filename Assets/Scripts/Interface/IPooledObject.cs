using UnityEngine;

public interface IPooledObject
{
    GameObject OnSpawn(Vector3 pos, Quaternion rot);
    void ReturnToPool(GameObject obj, GameObject parent);
}
