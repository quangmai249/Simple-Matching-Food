using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] Image imgBG;
    [SerializeField] Image loading;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] TextMeshProUGUI txtLevelCompleted;
    [SerializeField] TextMeshProUGUI txtLevelUncompleted;

    [Header("Button")]
    [SerializeField] Button btnReplay;
    [SerializeField] Button btnExitLose;

    private TileSpawner tileSpawner;
    private TimeManager timeManager;
    private DataLevel datalevel;
    private void Awake()
    {
        this.SetButtons();
        datalevel = LevelManager.Instance.DataLevel;
    }

    private void Start()
    {
        SaveManager.Instance.ChangeLanguage();

        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        timeManager = GameObject.FindGameObjectWithTag(TagName.TAG_TIME_MANAGER).GetComponent<TimeManager>();

        GameEvents.OnLevelChange.Raise(datalevel.levelName);

        StartCoroutine(nameof(CoroutineStartGame));
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

    private void SetButtons()
    {
        btnReplay.onClick.AddListener(Replay);
        btnExitLose.onClick.AddListener(Exit);

        btnReplay.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        btnExitLose.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
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

    private void OnLevelChanged(string name)
    {
        txtLevel.text = name;
    }

    private void OnLeveCompleted(string name)
    {
        txtLevelCompleted.text = name;
        txtLevelUncompleted.text = name;
    }

    private IEnumerator CoroutineReplay()
    {
        GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>()?.SetDefault();

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        timeManager.SetTimeLimit();

        GameManager.Instance.RestartGame(1f);

        yield return StartCoroutine(timeManager.CoroutineRunTime(1f));
    }

    private IEnumerator CoroutineStartGame()
    {
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

    private IEnumerator CoroutineNextLevel()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(2.5f);

        LevelManager.Instance.CurrentLevel = LevelManager.Instance.GetListDataLevel.IndexOf(LevelManager.Instance.DataLevel) + 1;

        DataLevel nextLevel = LevelManager.Instance.DicDataLevel.Values.ElementAt(LevelManager.Instance.CurrentLevel);

        if (nextLevel != null)
        {
            LevelManager.Instance.DataLevel = nextLevel;
            yield return StartCoroutine(nameof(CoroutineReplay));
        }
        else
        {
            Debug.Log("Out of size List DataLevel");
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneName.SCENE_LEVEL);
    }

    public void Replay()
    {
        StartCoroutine(nameof(CoroutineReplay));
    }

    public void NextLevel()
    {
        StartCoroutine(nameof(CoroutineNextLevel));
    }

    public IEnumerator DisplayPanelWin()
    {
        PanelWinGame panelWinGame = UIManager.instance.GetPanel(EnumPanelType.LevelWin).GetComponent<PanelWinGame>();

        panelWinGame?.ResetImageStar();

        yield return new WaitForSeconds(.75f);

        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlayAudioClip(EnumAudioClip.Win);

        UIManager.instance.ShowPanel(EnumPanelType.LevelWin);

        yield return new WaitForSeconds(.75f);

        yield return StartCoroutine(panelWinGame?.SetImageStar());
    }
}
