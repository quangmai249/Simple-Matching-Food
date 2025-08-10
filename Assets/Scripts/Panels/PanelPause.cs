using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPause : MonoBehaviour
{
    [SerializeField] Button _btnGiveUp;
    [SerializeField] GameObject panelGiveUp;

    private void Awake()
    {
        this.SetButtons();
    }
    private void Start()
    {
        panelGiveUp.SetActive(false);
    }
    private void SetButtons()
    {
        _btnGiveUp.onClick.AddListener(GiveUp);
        _btnGiveUp.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }
    private void GiveUp()
    {
        panelGiveUp.SetActive(true);
    }
}
