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
    [SerializeField] Sprite spriteStar;
    [SerializeField] Sprite spriteStarDefault;

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
        LevelManager.Instance.Pool.Pool = new Queue<GameObject>();

        foreach (var item in LevelManager.Instance.DicDataLevel)
            LevelManager.Instance.Pool.ReturnToPool(item.Key, LevelManager.Instance.gameObject);

        btnPlay.GetComponent<Image>().DOFade(0f, .15f).OnComplete(() =>
        {
            AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButtonStart);
            SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
        });
    }

    public void SetImageStar(string levelName)
    {
        foreach (Transform item in panelStar.transform)
            item.GetComponent<Image>().sprite = spriteStarDefault;

        int countStar = SaveManager.Instance.GetLevelFromDataLevelSaving(levelName).starCount;

        for (int i = 0; i < countStar; i++)
            panelStar.transform.GetChild(i).GetComponent<Image>().sprite = spriteStar;
    }

    public string TextLevelName
    {
        set => txtLevelName.text = value;
    }
}
