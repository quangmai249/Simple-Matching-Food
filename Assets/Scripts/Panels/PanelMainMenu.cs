using Assets.Scrips.Manager;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _btnList;
    [SerializeField] Button _btnStart;
    [SerializeField] Button _btnInformation;
    [SerializeField] Button _btnExit;
    [SerializeField] Button _btnSettings;
    [SerializeField] Button _btnCollections;

    [Header("GameObjects")]
    [SerializeField] GameObject _imageNotify;
    [SerializeField] GameObject _panelMainMenu;

    private HomeScene homeScene;

    private void Awake()
    {
        this.SetButtons();
    }

    private void Start()
    {
        homeScene = GameObject.FindGameObjectWithTag(TagName.TAG_HOME_SCENE).GetComponent<HomeScene>();

        _panelMainMenu.SetActive(false);

        _imageNotify.SetActive(GameManager.Instance.IsCollectNewTile);
    }

    private void Settings()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Settings);
    }

    private void Exit()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Exit);
    }

    private void SetButtons()
    {
        _btnExit.onClick.AddListener(Exit);
        _btnStart.onClick.AddListener(StartGame);
        _btnList.onClick.AddListener(ListMainMenu);
        _btnSettings.onClick.AddListener(Settings);
        _btnInformation.onClick.AddListener(Information);
        _btnCollections.onClick.AddListener(Collections);

        _btnExit.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnList.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnSettings.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnStart.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButtonStart));
        _btnInformation.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnCollections.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    private void Information()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Information);
    }

    private void ListMainMenu()
    {
        _panelMainMenu.SetActive(!_panelMainMenu.activeSelf);

        if (_panelMainMenu.activeSelf)
        {
            _panelMainMenu.GetComponent<Animator>().Play(KeyAnim.KEY_TRANSLATE_PANEL_LIST);
            _btnList.image.color = new Color(1, 1, 1, .25f);
        }
        else
            _btnList.image.color = new Color(1, 1, 1, 1);
    }

    private void StartGame()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);

        homeScene.StartGame();
    }

    private void Collections()
    {
        if (_panelMainMenu.activeSelf)
            this.ListMainMenu();

        if (GameManager.Instance.IsCollectNewTile)
        {
            GameManager.Instance.IsCollectNewTile = false;
            _imageNotify.SetActive(GameManager.Instance.IsCollectNewTile);
        }

        UIManager.instance.ShowPanel(EnumPanelType.Collections);
    }
}
