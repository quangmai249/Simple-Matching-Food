using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelSelectLevel : MonoBehaviour
{
    [SerializeField] Button btnPlay;
    [SerializeField] GameObject panelStar;
    [SerializeField] TextMeshProUGUI txtLevelName;
    private void Awake()
    {
        this.SetDefault();
    }
    private void SetDefault()
    {
        btnPlay.onClick.AddListener(StartPlay);
        btnPlay.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButtonStart));
    }
    private void StartPlay()
    {
        btnPlay.GetComponent<Image>().DOFade(0f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
        });
    }
    public string TextLevelName
    {
        set => txtLevelName.text = value;
    }
}
