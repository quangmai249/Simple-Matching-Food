using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] List<Sprite> lsBackImg = new List<Sprite>();
    [SerializeField] List<Sprite> lsFrontImg = new List<Sprite>();
    [SerializeField] List<Sprite> lsFrontImg2 = new List<Sprite>();

    private int _row, _col, _maxTile, _maxSprite;

    private GameObject _tile;
    private GameObject _gridTileGroup;

    private Gameplay gameplay;

    private Queue<int> queue = new Queue<int>();

    private void Awake()
    {
        gameplay = GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>();
    }

    private void Start()
    {
        _gridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);

        this.SetRandomTileLevel();
        this.SpawnTile();
    }

    private void SetRandomTileLevel()
    {
        _col = Random.Range(2, 7);
        _row = Random.Range(2, 11);

        if (_col % 2 != 0)
            _col++;

        _maxTile = _col * _row;
        _maxSprite = _maxTile / gameplay.CountMatching;

        int[] arr = new int[_maxTile];

        HashSet<int> hashSet = new HashSet<int>();

        while (hashSet.Count < _maxSprite)
        {
            hashSet.Add(Random.Range(0, lsFrontImg2.Count - 1));
        }

        hashSet.CopyTo(arr);
        hashSet.CopyTo(arr, arr.Length / 2);

        ShuffleArray(arr);

        foreach (var item in arr)
            queue.Enqueue(item);

        if (_col > 4)
            this.ReponsiveGrid(_col, _row);
    }

    private void SpawnTile()
    {
        _gridTileGroup.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridTileGroup.GetComponent<GridLayoutGroup>().constraintCount = _col;

        for (int i = 0; i < _maxTile; i++)
        {
            _tile = TilePool.Instance.Pool.OnSpawn(Vector3.zero, Quaternion.identity);
            _tile.transform.SetParent(_gridTileGroup.transform);

            SetSpriteImageTile();
        }
    }

    private void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    private void SetSpriteImageTile()
    {
        _tile.GetComponent<Tile>().SetBackImg(lsBackImg[0]);
        _tile.GetComponent<Tile>().SetFrontImg(lsFrontImg2[queue.Dequeue()]);
    }

    private void ReponsiveGrid(int columns, int rows)
    {
        Vector2 spacing = new Vector2(5f, 5f);
        Vector2 padding = new Vector2(10f, 10f);

        RectTransform rt = _gridTileGroup.GetComponent<RectTransform>();
        GridLayoutGroup grid = _gridTileGroup.GetComponent<GridLayoutGroup>();

        float cell = (rt.rect.width - padding.x * 2 - spacing.x * (columns - 1)) / columns;

        grid.cellSize = new Vector2(cell, cell);
        grid.spacing = spacing;
        grid.padding = new RectOffset(
            Mathf.RoundToInt(padding.x),
            Mathf.RoundToInt(padding.x),
            Mathf.RoundToInt(padding.y),
            Mathf.RoundToInt(padding.y)
        );
    }

    public int MaxTile
    {
        get => _maxTile;
    }
}
