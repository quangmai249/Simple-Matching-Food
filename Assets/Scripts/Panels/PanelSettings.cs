using Assets.Scrips.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MusicSlider;

    [SerializeField] Button _btnConfirm;
    [SerializeField] Button _btnCancel;
    [SerializeField] Transform _panelLanguages;

    private Dictionary<EnumLanguages, Button> languages = new Dictionary<EnumLanguages, Button>();
    private Button _languageClicked;

    private void Awake()
    {
        this.Register();
        this.SetButtons();
    }

    private void Start()
    {
        SaveManager.Instance.ChangeLanguage();
    }

    private void Update()
    {
        AudioManager.Instance.SetVolume(SFXSlider.value, MusicSlider.value);
    }

    private void OnEnable()
    {
        DataSetting dataSetting = SaveManager.Instance.GetDataSetting();

        SFXSlider.value = dataSetting.sfx;
        MusicSlider.value = dataSetting.music;

        foreach (var item in languages)
        {
            if (dataSetting.enumLanguages == item.Key)
            {
                this.ChangeLanguage(item.Value);
                return;
            }
        }

        this.ChangeLanguage(languages.FirstOrDefault().Value);
    }

    private void OnDisable()
    {
        DataSetting dataSetting = SaveManager.Instance.GetDataSetting();
        AudioManager.Instance.SetVolume(dataSetting.Sfx, dataSetting.Music);
    }

    private void Register()
    {
        foreach (Transform item in _panelLanguages)
        {
            if (Enum.TryParse(item.name, out EnumLanguages enumLanguages))
                languages[enumLanguages] = item.gameObject.GetComponent<Button>();
        }

        foreach (var item in languages)
            item.Value.onClick.AddListener(() =>
            {
                ChangeLanguage(item.Value);
            });
    }

    private void SetButtons()
    {
        _btnCancel.onClick.AddListener(Cancel);
        _btnConfirm.onClick.AddListener(Confirm);

        _btnCancel.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnConfirm.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    private void Confirm()
    {
        SaveManager.Instance.SaveDataSetting(SFXSlider.value, MusicSlider.value, _languageClicked.name);
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
        SaveManager.Instance.ChangeLanguage();
    }

    private void Cancel()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }

    private void ChangeLanguage(Button btn)
    {
        foreach (var item in languages)
            item.Value.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1);

        btn.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        _languageClicked = btn;
    }
}
