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
        foreach (Transform item in panelStar.transform)
            item.GetComponent<Image>().sprite = spriteNoneStar;
    }
    public IEnumerator SetImageStar()
    {
        float maxTime = timeManager.MaxTimeLimit;
        float timeRemaning = timeManager.TimeLimit;

        if (timeRemaning >= 2 * maxTime / 3)
        {
            yield return new WaitForSeconds(0.15f);
            panelStar.transform.GetChild(0).GetComponent<Image>().sprite = spriteStar;
            yield return new WaitForSeconds(0.25f);
            panelStar.transform.GetChild(1).GetComponent<Image>().sprite = spriteStar;
            yield return new WaitForSeconds(0.35f);
            panelStar.transform.GetChild(2).GetComponent<Image>().sprite = spriteStar;
        }
        else if (timeRemaning >= maxTime / 3)
        {
            yield return new WaitForSeconds(0.15f);
            panelStar.transform.GetChild(0).GetComponent<Image>().sprite = spriteStar;
            yield return new WaitForSeconds(0.25f);
            panelStar.transform.GetChild(1).GetComponent<Image>().sprite = spriteStar;
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
            panelStar.transform.GetChild(0).GetComponent<Image>().sprite = spriteStar;
        }

        yield return new WaitForSeconds(.25f);
        panelButtons.SetActive(true);
    }
}
