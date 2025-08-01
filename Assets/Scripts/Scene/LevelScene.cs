using Assets.Scrips.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScene : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] GameObject panelLevel;
    [SerializeField] GameObject panelSelectLevel;

    private bool firstStage;
    private Dictionary<GameObject, DataLevel> dic = new Dictionary<GameObject, DataLevel>();
    private void Start()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Stage);

        this.SetButtonLevel();

        firstStage = true;
        this.SetActiveButtonLevel();

        btn = GameObject.FindGameObjectWithTag(TagName.BUTTON_NEXT_STAGE).GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton);

            firstStage = !firstStage;
            this.SetActiveButtonLevel();
        });
    }
    private void SetButtonLevel()
    {
        dic = LevelManager.Instance.DicDataLevel;

        foreach (var item in dic.Keys)
            item.gameObject.transform.SetParent(panelLevel.transform);

        foreach (var item in dic)
        {
            if (IsUnlocked(item.Value.levelName))
                item.Value.isUnlocked = true;

            item.Key.GetComponent<ButtonLevel>().Data = item.Value;

            item.Key.GetComponent<ButtonLevel>().SetDefault();

            item.Key.GetComponent<Button>().onClick.AddListener(() => SetDataLevel(item.Key));
            item.Key.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        }
    }
    private void SetActiveButtonLevel()
    {
        int b = 0;
        int count = dic.Keys.Count;

        if (!firstStage)
        {
            foreach (var item in dic)
            {
                if (b < count / 2)
                    item.Key.gameObject.SetActive(false);
                else
                    item.Key.gameObject.SetActive(true);
                b++;
            }
        }
        else
        {
            foreach (var item in dic)
            {
                if (b < count / 2)
                    item.Key.gameObject.SetActive(true);
                else
                    item.Key.gameObject.SetActive(false);
                b++;
            }
        }
    }
    private void SetDataLevel(GameObject btn)
    {
        GameObject.FindGameObjectWithTag(TagName.BUTTON_NEXT_STAGE).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.FindGameObjectWithTag(TagName.BUTTON_NEXT_STAGE).GetComponent<Button>().interactable = false;

        LevelManager.Instance.DataLevel = btn.GetComponent<ButtonLevel>().Data;

        UIManager.instance.ShowPanel(EnumPanelType.SelectLevel);

        panelSelectLevel = GameObject.FindGameObjectWithTag(TagName.PANEL_SELECT_LEVEL);
        panelSelectLevel.GetComponent<PanelSelectLevel>().TextLevelName = LevelManager.Instance.DataLevel.levelName;
        panelSelectLevel.GetComponent<PanelSelectLevel>().SetImageStar(LevelManager.Instance.DataLevel.levelName);
    }
    private bool IsUnlocked(string levelName)
    {
        foreach (var item in LevelManager.Instance.HashSetLevelUnLocked)
            if (levelName == item)
                return true;

        return false;
    }
}
