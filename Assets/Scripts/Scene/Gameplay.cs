using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField] bool _isChecking;
    [SerializeField] int _countMatching = 2;
    [SerializeField] int _countTileMatched;

    private int _count_arr;
    private Tile[] arr = new Tile[10];

    private TileSpawner tileSpawner;
    private GameObject _gridTileGroup;

    private void Awake()
    {
        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        _gridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);
    }

    private void Start()
    {
        this.SetDefault();
        UIManager.instance.ShowPanel(EnumPanelType.HUD);
    }

    private void OnEnable()
    {
        GameEvents.OnTileSelected.Register(Selected);
    }

    private void OnDisable()
    {
        GameEvents.OnTileSelected.Unregister(Selected);
    }

    private void Selected(Tile tile)
    {
        arr[_count_arr] = tile;
        _count_arr++;

        if (_count_arr == _countMatching)
        {
            _isChecking = true;

            if (IsMatching(_countMatching))
                this.Matched();
            else
                StartCoroutine(nameof(this.CoroutineNotMatched));
        }
    }

    private void Matched()
    {
        for (int i = 0; i < _count_arr; i++)
        {
            arr[i].IsMatched = true;
            arr[i].GetComponent<Image>().color = new Color(1, 1, 1, .15f);
        }

        _count_arr = 0;

        _isChecking = false;

        _countTileMatched += _countMatching;

        if (_countTileMatched == tileSpawner.MaxTile)
            StartCoroutine(nameof(this.CheckWinGame));
    }

    private IEnumerator CheckWinGame()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            item.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.5f);

        _gridTileGroup.transform
            .DOScale(1.25f, 0.25f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);

        yield return
            StartCoroutine(GameManager.Instance.RestartGame(3, 1));
    }

    private IEnumerator CoroutineNotMatched()
    {
        for (int i = 0; i < _count_arr; i++)
            arr[i].GetComponent<Animator>().SetBool(KeyAnim.KeyTileMatch, true);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < _count_arr; i++)
            arr[i].GetComponent<Animator>().SetBool(KeyAnim.KeyTileMatch, false);

        for (int i = 0; i < _count_arr; i++)
        {
            arr[i].IsFlipped = false;
            arr[i].Flip();
            arr[i].IsChecking = false;
        }

        _count_arr = 0;

        yield return new WaitForSeconds(.25f);

        _isChecking = false;
    }

    private void SetDefault()
    {
        _isChecking = false;
        _countTileMatched = 0;
    }

    private bool IsMatching(int countTileMatching)
    {
        Sprite t = arr[0].GetFrontImg();
        for (int i = 1; i < countTileMatching; i++)
            if (t != arr[i].GetFrontImg())
                return false;
        return true;
    }

    public bool IsChecking { get => _isChecking; }
    public int CountMatching { get => _countMatching; }
}
