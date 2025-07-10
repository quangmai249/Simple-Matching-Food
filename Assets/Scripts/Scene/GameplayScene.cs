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
    [SerializeField] Image imgBG;
    [SerializeField] Image loading;

    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] TextMeshProUGUI txtLevelCompleted;

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

        btnNextLevel.onClick.AddListener(NextLevel);
        btnExit.onClick.AddListener(Exit);

        btnReplay.onClick.AddListener(Replay);
        btnExitLose.onClick.AddListener(Exit);
    }

    private IEnumerator Start()
    {

        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(2f);

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        tileSpawner.GridTileGroup = GameObject.FindGameObjectWithTag(TagName.TAG_GRID_TILE_GROUP);
        tileSpawner.GridCellSizeDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().cellSize;
        tileSpawner.GridSpacingDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().spacing;
        tileSpawner.GridPaddingDF = tileSpawner.GridTileGroup.GetComponent<GridLayoutGroup>().padding;

        this.AnimBegin();

        tileSpawner.StartCoroutine(tileSpawner.SetDefault(2f));
        timeManager.SetTimeLimit();

        StartCoroutine(timeManager.CoroutineRunTime(2f));
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
            txtLevelCompleted.text = LevelManager.Instance.GetDataLevel(level).levelName + " completed!";
        else
            txtLevelCompleted.text = null;
    }

    private void NextLevel()
    {
        StartCoroutine(nameof(CoroutineNextLevel));
    }

    private IEnumerator CoroutineNextLevel()
    {
        timeManager.SetTimeLimit();

        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(3f);

        GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>()?.SetDefault();

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        GameManager.Instance.RestartGame(.5f);

        StartCoroutine(timeManager.CoroutineRunTime(.5f));
    }

    private void Replay()
    {
        GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>()?.SetDefault();

        timeManager.SetTimeLimit();

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        GameManager.Instance.RestartGame(.5f);

        StartCoroutine(timeManager.CoroutineRunTime(.5f));
    }

    private void Exit()
    {
        SceneManager.LoadScene(SceneName.SCENE_HOME);
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
