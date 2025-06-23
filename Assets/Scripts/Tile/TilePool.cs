using UnityEngine;

public class TilePool : Singleton<TilePool>
{
    [SerializeField] int tilePoolSize = 100;
    [SerializeField] GameObject tilePrefab;

    private ObjectPool _pool;

    protected override void Awake()
    {
        base.Awake();

        _pool = new ObjectPool(tilePrefab, this.transform, tilePoolSize);
    }

    public ObjectPool Pool
    {
        get => _pool;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
