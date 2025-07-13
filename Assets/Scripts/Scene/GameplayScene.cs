using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] Image imgBG;
    [SerializeField] Image loading;

    [Header("Stars")]
    [SerializeField] GameObject panelStar;
    [SerializeField] Sprite spriteStar;
    [SerializeField] Sprite spriteNoneStar;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] TextMeshProUGUI txtLevelCompleted;
    [SerializeField] TextMeshProUGUI txtLevelUncompleted;

    [Header("Button")]
    [SerializeField] Button btnNextLevel;
    [SerializeField] Button btnExit;
    [SerializeField] Button btnReplay;
    [SerializeField] Button btnExitLose;

    private TileSpawner tileSpawner;
    private TimeManager timeManager;

    private void Awake()
    {
        SaveManager.Instance.ChangeLanguage();

        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        timeManager = GameObject.FindGameObjectWithTag(TagName.TAG_TIME_MANAGER).GetComponent<TimeManager>();

        this.SetButtonDefault();
    }

    private IEnumerator Start()
    {
        GameEvents.OnLevelChange.Raise(tileSpawner.CurrentLevel);

        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(2f);

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        tileSpawner.GridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);
        tileSpawner.GridCellSizeDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().cellSize;
        tileSpawner.GridSpacingDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().spacing;
        tileSpawner.GridPaddingDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().padding;

        this.AnimBegin();

        timeManager.SetTimeLimit();

        yield return StartCoroutine(timeManager.CoroutineRunTime(2.5f));
    }

    private void OnEnable()
    {
        GameEvents.OnLevelChange.Register(OnLevelChanged);
        GameEvents.OnLevelChange.Register(OnLeveCompleted);
    }

    private void OnDestroy()
    {
        GameEvents.OnLevelChange.Clear();
    }

    private void OnLevelChanged(int level)
    {
        txtLevel.text = LevelManager.Instance.GetDataLevel(level).levelName;
    }

    private void OnLeveCompleted(int level)
    {
        if (level >= 0)
        {
            txtLevelCompleted.text = LevelManager.Instance.GetDataLevel(level).levelName;
            txtLevelUncompleted.text = LevelManager.Instance.GetDataLevel(level).levelName;
        }
        else
        {
            txtLevelCompleted.text = null;
            txtLevelUncompleted.text = null;
        }
    }

    private void SetButtonDefault()
    {
        btnExit.onClick.AddListener(Exit);
        btnReplay.onClick.AddListener(Replay);
        btnExitLose.onClick.AddListener(Exit);
        btnNextLevel.onClick.AddListener(NextLevel);

        btnExit.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        btnReplay.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        btnExitLose.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        btnNextLevel.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    private void NextLevel()
    {
        StartCoroutine(nameof(CoroutineNextLevel));
    }

    private void Replay()
    {
        StartCoroutine(nameof(CoroutineReplay));
    }

    private IEnumerator CoroutineNextLevel()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(CoroutineReplay());
    }

    private IEnumerator CoroutineReplay()
    {
        GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>()?.SetDefault();

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        timeManager.SetTimeLimit();
        GameManager.Instance.DisableAllTiles();
        GameEvents.OnLevelChange.Raise(tileSpawner.CurrentLevel);

        yield return StartCoroutine(timeManager.CoroutineRunTime(1f));
    }

    private void Exit()
    {
        SceneManager.LoadScene(SceneName.SCENE_HOME);
    }

    private void ResetImageStar()
    {
        foreach (Transform item in panelStar.transform)
            item.GetComponent<Image>().sprite = spriteNoneStar;
    }

    private IEnumerator SetImageStar(float timePlayed)
    {
        float max = LevelManager.Instance.GetDataLevel(tileSpawner.CurrentLevel).timeLimit;

        if (timePlayed >= max * 2 / 3)
        {
            foreach (Transform item in panelStar.transform)
            {
                yield return new WaitForSeconds(0.25f);
                item.GetComponent<Image>().sprite = spriteStar;
            }
        }
        else if (timePlayed >= max / 3)
        {
            yield return new WaitForSeconds(0.25f);
            panelStar.transform.GetChild(0).GetComponent<Image>().sprite = spriteStar;
            yield return new WaitForSeconds(0.25f);
            panelStar.transform.GetChild(1).GetComponent<Image>().sprite = spriteStar;
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            panelStar.transform.GetChild(0).GetComponent<Image>().sprite = spriteStar;
        }
    }

    public IEnumerator DisplayPanelWin()
    {
        this.ResetImageStar();

        yield return new WaitForSeconds(.75f);

        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlayAudioClip(EnumAudioClip.Win);

        UIManager.instance.ShowPanel(EnumPanelType.LevelWin);

        yield return new WaitForSeconds(.75f);

        yield return StartCoroutine(SetImageStar(timeManager.TimeLimit));
    }

    private void AnimBegin()
    {
        imgBG.DOFade(1f, 1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                imgBG.transform.DOScale(new Vector3(2, 2, 2), 1.5f)
                .SetEase(Ease.InOutSine);

                imgBG.DOFade(0.1f, 1f)
                .SetEase(Ease.InSine);
            });
    }
}
