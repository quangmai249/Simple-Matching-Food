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

    private List<Tile> lsTile = new List<Tile>();
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
        lsTile.Add(tile);

        if (lsTile.Count == _countMatching)
        {
            _isChecking = true;

            if (IsMatching(_countMatching))
                StartCoroutine(nameof(this.CoroutineMatched));
            else
                StartCoroutine(nameof(this.CoroutineNotMatched));
        }
    }

    private IEnumerator CoroutineMatched()
    {
        yield return null;

        lsTile.ForEach(t =>
        {
            t.IsMatched = true;
            t.GetComponent<Image>().color = new Color(1, 1, 1, .25f);
        });

        lsTile.Clear();

        _isChecking = false;

        _countTileMatched += _countMatching;

        if (IsWinGame())
        {
            yield return new WaitForSeconds(3f);
            this.DisableAllTilesMatched();
            yield return new WaitForSeconds(1f);
            this.LoadNextLevel();
        }
    }

    private bool IsWinGame()
    {
        if (_countTileMatched == tileSpawner.MaxTile)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
                item.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gridTileGroup.transform
                .DOScale(1.25f, 0.25f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad);

            return true;
        }

        return false;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisableAllTilesMatched()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
        {
            item.GetComponent<Tile>().SetDefault();
            TilePool.Instance.Pool.ReturnToPool(item, TilePool.Instance.gameObject);
        }

        Debug.Log($"Count after return {TilePool.Instance.name} : {TilePool.Instance.gameObject.transform.childCount}");
    }

    private IEnumerator CoroutineNotMatched()
    {
        lsTile.ForEach(t =>
        {
            t.GetComponent<Animator>().SetBool(KeyAnim.KeyTileMatch, true);
        });

        yield return new WaitForSeconds(1f);

        lsTile.ForEach(t =>
        {
            t.GetComponent<Animator>().SetBool(KeyAnim.KeyTileMatch, false);
        });

        foreach (Tile t in lsTile)
        {
            t.IsFlipped = false;
            t.Flip();
            t.IsChecking = false;
        }

        lsTile.Clear();

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
        Sprite t = lsTile[0].GetFrontImg();

        for (int i = 1; i < lsTile.Count; i++)
            if (lsTile[i].GetFrontImg() != t)
                return false;

        return true;
    }

    public bool IsChecking { get => _isChecking; }
    public int CountMatching { get => _countMatching; }
}
