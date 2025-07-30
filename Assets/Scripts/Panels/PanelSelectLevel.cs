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
    }
    private void StartPlay()
    {
        btnPlay.GetComponent<Image>().DOFade(0f, .15f).OnComplete(() =>
        {
            foreach (var item in LevelManager.Instance.DicDataLevel)
                LevelManager.Instance.Pool.ReturnToPool(item.Key, LevelManager.Instance.gameObject);

            SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
        });
    }
    public string TextLevelName
    {
        set => txtLevelName.text = value;
    }
}
