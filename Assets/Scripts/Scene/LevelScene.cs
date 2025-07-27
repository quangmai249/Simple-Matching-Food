using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScene : MonoBehaviour
{
    [SerializeField] GameObject panelLevel;
    [SerializeField] GameObject panelSelectLevel;
    [SerializeField] GameObject buttonLevel;

    private GameObject _btn;
    private List<DataLevel> lsDataLevel = new List<DataLevel>();
    private Dictionary<GameObject, DataLevel> dic = new Dictionary<GameObject, DataLevel>();
    private void Start()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Stage);
        this.SetButtonLevel();
    }
    private void SetButtonLevel()
    {
        lsDataLevel = LevelManager.Instance.GetListDataLevel;

        foreach (var item in lsDataLevel)
        {
            _btn = Instantiate(buttonLevel);

            _btn.name = item.levelName;
            _btn.gameObject.transform.SetParent(panelLevel.transform);
            _btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.levelName.Replace("Level ", "");

            dic[_btn] = item;
        }

        foreach (var item in dic)
        {
            item.Key.GetComponent<ButtonLevel>().Data = item.Value;
            item.Key.GetComponent<ButtonLevel>().SetDefault();

            item.Key.GetComponent<Button>().onClick.AddListener(() => SetDataLevel(item.Key));
            item.Key.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        }
    }
    private void SetDataLevel(GameObject btn)
    {
        UIManager.instance.ShowPanel(EnumPanelType.SelectLevel);

        LevelManager.Instance.DataLevel = btn.GetComponent<ButtonLevel>().Data;

        panelSelectLevel.GetComponent<PanelSelectLevel>().TextLevelName = LevelManager.Instance.DataLevel.levelName;
    }
}
