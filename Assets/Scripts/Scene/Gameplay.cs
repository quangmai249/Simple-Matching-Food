using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] bool _isStarted;
    [SerializeField] bool _isChecking;
    [SerializeField] bool _isWinGame;
    [SerializeField] bool _isLoseGame;
    [SerializeField] int _countTileMatched;

    private int _count_arr, _countMatching;
    private Tile[] arr = new Tile[10];

    private TileSpawner tileSpawner;
    private GameplayScene gameplayScene;

    private void Awake()
    {
        this.SetDefault();
    }

    private void Start()
    {
        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        gameplayScene = GameObject.FindGameObjectWithTag(SceneName.SCENE_GAMEPLAY).GetComponent<GameplayScene>();
    }

    private void OnEnable()
    {
        GameEvents.OnTileSelected.Register(this.Selected);
    }

    private void OnDestroy()
    {
        GameEvents.OnTileSelected.Clear();
    }

    private void Selected(Tile tile)
    {
        arr[_count_arr] = tile;

        _count_arr++;

        _countMatching = tileSpawner.CountMatching;

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
        AudioManager.Instance.PlayAudioClip(EnumAudioClip.Matched);

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
        _isStarted = false;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            item.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.5f);

        GameObject _gridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);

        if (_gridTileGroup != null)
        {
            _gridTileGroup.transform
                .DOScale(1.25f, 0.25f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
                        SaveManager.Instance.SaveDataCollection(item.GetComponent<Image>().sprite.name);

                    StartCoroutine(nameof(UnlockNextLevel));
                });
        }
    }

    private IEnumerator UnlockNextLevel()
    {
        yield return StartCoroutine(gameplayScene.DisplayPanelWin());

        int index = LevelManager.Instance.GetListDataLevel.IndexOf(LevelManager.Instance.DataLevel);
        int starCount = GameObject.FindGameObjectWithTag(TagName.PANEL_WIN_GAME).GetComponent<PanelWinGame>().CountStar;

        string currentLevelName = LevelManager.Instance.GetListDataLevel[index].levelName;
        string nextLevelName = LevelManager.Instance.GetListDataLevel[index + 1].levelName;

        if (LevelManager.Instance.HashSetLevelUnLocked.Contains(currentLevelName))
        {
            if (SaveManager.Instance.GetLevelFromDataLevelSaving(currentLevelName).starCount < starCount)
                SaveManager.Instance.SaveDataLevel(new Level(starCount, currentLevelName));
        }

        if (nextLevelName != "")
        {
            if (!LevelManager.Instance.HashSetLevelUnLocked.Contains(nextLevelName))
            {
                SaveManager.Instance.SaveDataLevel(new Level(0, nextLevelName));

                LevelManager.Instance.HashSetLevelUnLocked.Add(nextLevelName);
                LevelManager.Instance.DicDataLevel.ElementAt(index + 1).Value.isUnlocked = true;
            }
        }
        else
        {
            Debug.LogWarning(nextLevelName + " is null!");
        }
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

        if (!_isLoseGame)
            AudioManager.Instance.PlayAudioClip(EnumAudioClip.NotMatched);

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
        _isStarted = false;
        _isChecking = false;
        _isWinGame = false;
        _isLoseGame = false;
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

    public bool IsStarted { get => _isStarted; set => _isStarted = value; }

    public bool IsWinGame { get => _isWinGame; }

    public bool IsLoseGame { set => _isLoseGame = value; }

    public bool IsChecking { get => _isChecking; }
}
