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
    [SerializeField] bool _isWinGame;
    [SerializeField] int _countMatching = 2;
    [SerializeField] int _countTileMatched;

    private int _count_arr;
    private Tile[] arr = new Tile[10];

    private TileSpawner tileSpawner;
    private GameObject _gridTileGroup;

    private void Start()
    {
        this.SetDefault();

        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        _gridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);
    }

    private void OnEnable()
    {
        GameEvents.OnTileSelected.Register(this.Selected);
    }

    private void OnDisable()
    {
        GameEvents.OnTileSelected.Unregister(this.Selected);
    }

    private void Selected(Tile tile)
    {
        arr[_count_arr] = tile;
        _count_arr++;

        if (_count_arr == _countMatching)
        {
            _isChecking = true;

            if (IsMatching(_countMatching))
            {
                StartCoroutine(nameof(this.CoroutineMatched));
            }
            else
            {
                StartCoroutine(nameof(this.CoroutineNotMatched));
            }
        }
    }

    private IEnumerator CoroutineMatched()
    {
        AudioManager.Instance.TileMatched();

        for (int i = 0; i < _count_arr; i++)
        {
            arr[i].IsMatched = true;
            arr[i].GetComponent<Image>().color = new Color(1, 1, 1, .15f);
        }

        _count_arr = 0;

        _isChecking = false;

        _countTileMatched += _countMatching;

        if (_countTileMatched == tileSpawner.MaxTile)
        {
            yield return StartCoroutine(nameof(this.WinGame));
        }
    }

    private IEnumerator WinGame()
    {
        _isWinGame = true;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            item.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.5f);

        if (_gridTileGroup != null)
        {
            _gridTileGroup.transform
                .DOScale(1.25f, 0.25f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    StartCoroutine(nameof(this.CoroutineNextLevel));
                });
        }
    }

    private IEnumerator CoroutineNextLevel()
    {
        yield return new WaitForSeconds(1f);
        this.SetDefault();
        GameManager.Instance.RestartGame(1f);
    }

    private IEnumerator CoroutineNotMatched()
    {
        yield return StartCoroutine(nameof(CoroutineShakeTile), 1f);

        for (int i = 0; i < _count_arr; i++)
        {
            arr[i].IsFlipped = false;
            arr[i].Flip();
            arr[i].IsChecking = false;
        }

        AudioManager.Instance.TileNotMatched();

        _count_arr = 0;

        yield return new WaitForSeconds(.25f);

        _isChecking = false;
    }

    private IEnumerator CoroutineShakeTile(float timeShake)
    {
        for (int i = 0; i < _count_arr; i++)
            arr[i].GetComponent<Animator>().SetBool(KeyAnim.KEY_TILE_MATCH, true);

        yield return new WaitForSeconds(timeShake);

        for (int i = 0; i < _count_arr; i++)
            arr[i].GetComponent<Animator>().SetBool(KeyAnim.KEY_TILE_MATCH, false);

    }

    public void SetDefault()
    {
        _isWinGame = false;
        _isChecking = false;
        _count_arr = 0;
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

    public bool IsWinGame { get => _isWinGame; }
    public bool IsChecking { get => _isChecking; }
    public int CountMatching { get => _countMatching; }
}
