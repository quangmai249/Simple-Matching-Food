using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelWinGame : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject panelButtons;
    [SerializeField] Button btnNextLevel;
    [SerializeField] Button btnExit;

    [Header("Stars")]
    [SerializeField] int countStar;
    [SerializeField] GameObject panelStar;
    [SerializeField] Sprite spriteStar;
    [SerializeField] Sprite spriteNoneStar;

    private TimeManager timeManager;
    private GameplayScene gameplayScene;

    private void Awake()
    {
        this.SetButtons();
    }

    private void Start()
    {
        timeManager = GameObject.FindGameObjectWithTag(TagName.TAG_TIME_MANAGER).GetComponent<TimeManager>();
        gameplayScene = GameObject.FindGameObjectWithTag(SceneName.SCENE_GAMEPLAY).GetComponent<GameplayScene>();
    }

    private void NextLevel()
    {
        if ((LevelManager.Instance.CurrentLevel + 1) == LevelManager.Instance.Pool.Pool.Count)
        {
            this.Exit();
            return;
        }

        gameplayScene.NextLevel();
    }

    private void Exit()
    {
        gameplayScene.Exit();
    }

    private void SetButtons()
    {
        btnExit.onClick.AddListener(Exit);
        btnNextLevel.onClick.AddListener(NextLevel);

        btnExit.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        btnNextLevel.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    public void ResetImageStar()
    {
        panelButtons.SetActive(false);

        foreach (Transform item in panelStar.transform)
            item.GetComponent<Image>().sprite = spriteNoneStar;
    }

    public IEnumerator SetImageStar()
    {
        float maxTime = timeManager.MaxTimeLimit;
        float timeRemaning = timeManager.TimeLimit;

        if (timeRemaning / maxTime > 0.75f)
            countStar = 3;
        else if (timeRemaning / maxTime > 0.25f)
            countStar = 2;
        else
            countStar = 1;

        for (int i = 0; i < countStar; i++)
        {
            yield return new WaitForSeconds(0.15f);
            panelStar.transform.GetChild(i).GetComponent<Image>().sprite = spriteStar;
        }

        yield return new WaitForSeconds(1f);
        panelButtons.SetActive(true);
    }

    public int CountStar { get => countStar; }
}
