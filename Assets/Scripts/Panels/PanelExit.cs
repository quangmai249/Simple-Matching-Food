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

    void Start()
    {
        _btnCancel.onClick.AddListener(Cancel);
        _btnConfirm.onClick.AddListener(Confirm);
    }

    private void OnEnable()
    {
        SaveManager.Instance.ChangeLanguage();
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
