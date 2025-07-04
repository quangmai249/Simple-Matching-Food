using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInformation : MonoBehaviour
{
    [SerializeField] Button _btnConfirm;

    private void Start()
    {
        _btnConfirm.onClick.AddListener(Confirm);
    }
    private void OnEnable()
    {
        SaveManager.Instance.ChangeLanguage();
    }

    private void Confirm()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }
}
