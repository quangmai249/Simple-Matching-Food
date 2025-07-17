using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInformation : MonoBehaviour
{
    [SerializeField] Button _btnConfirm;
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
        _btnConfirm.onClick.AddListener(Confirm);
        _btnConfirm.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }
    private void Confirm()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }
}
