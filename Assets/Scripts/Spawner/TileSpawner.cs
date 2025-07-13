using Assets.Scrips.Manager;
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

    private int _row, _col, _maxTile, _maxSprite, _countMatching;

    private int _level;

    private GameObject _tile, _gridTileGroup;

    private Queue<int> queue = new Queue<int>();

    // Grid properties
    private Vector2 gridCellSizeDF, gridSpacingDF;
    private RectOffset gridPaddingDF;

    private void Start()
    {
        _level = 0;
    }

    public void SetDefault()
    {

        this.SetRandomTileLevel();
        this.SpawnTile();
    }

    private void SetRandomTileLevel()
    {
        _col = (int)LevelManager.Instance.GetDataLevel(_level).levelSize.x;
        _row = (int)LevelManager.Instance.GetDataLevel(_level).levelSize.y;

        _countMatching = LevelManager.Instance.GetDataLevel(_level).matchingCount;

        _maxTile = _col * _row;

        _maxSprite = _maxTile / _countMatching;

        int[] arr = new int[_maxTile];

        HashSet<int> hashSet = new HashSet<int>();

        while (hashSet.Count < _maxSprite)
        {
            hashSet.Add(Random.Range(0, lsFrontImg.Count - 1));
        }

        if (_countMatching % 2 == 0)
        {
            hashSet.CopyTo(arr);
            hashSet.CopyTo(arr, arr.Length / 2);
        }
        else
        {
            hashSet.CopyTo(arr);
            hashSet.CopyTo(arr, arr.Length / 3);
            hashSet.CopyTo(arr, arr.Length * 2 / 3);
        }

        ShuffleArray(arr);

        foreach (var item in arr)
            queue.Enqueue(item);

        if (_col > 6)
            this.ReponsiveGrid(_col, _row);
        else if (_gridTileGroup.GetComponent<GridLayoutGroup>().cellSize != this.gridCellSizeDF)
            this.DefaultReponsiveGrid();
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
        _tile.GetComponent<Tile>().SetFrontImg(lsFrontImg[queue.Dequeue()]);
    }

    private void ReponsiveGrid(int columns, int rows)
    {
        _gridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);

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

    private void DefaultReponsiveGrid()
    {
        _gridTileGroup.GetComponent<GridLayoutGroup>().cellSize = this.gridCellSizeDF;
        _gridTileGroup.GetComponent<GridLayoutGroup>().spacing = this.gridSpacingDF;
        _gridTileGroup.GetComponent<GridLayoutGroup>().padding = this.gridPaddingDF;
    }

    public int CurrentLevel
    {
        get => _level;
        set => _level = value;
    }

    public int CountMatching
    {
        get => _countMatching;
    }

    public GameObject GridTileGroup
    {
        get => _gridTileGroup;
        set => _gridTileGroup = value;
    }

    public Vector2 GridCellSizeDF
    {
        set => gridCellSizeDF = value;
    }

    public Vector2 GridSpacingDF
    {
        set => gridSpacingDF = value;
    }

    public RectOffset GridPaddingDF
    {
        set => gridPaddingDF = value;
    }

    public int MaxTile
    {
        get => _maxTile;
    }
}
