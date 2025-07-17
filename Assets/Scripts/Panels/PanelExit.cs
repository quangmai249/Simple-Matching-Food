using Assets.Scrips.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelExit : MonoBehaviour
{
    [SerializeField] Button _btnConfirm;
    [SerializeField] Button _btnCancel;
    private void Awake()
    {
        this.SetButtons();
    }
    private void OnEnable()
    {
        SaveManager.Instance.ChangeLanguage();
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
        Application.Quit();
    }
    private void Cancel()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }
}
