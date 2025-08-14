using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGiveUp : MonoBehaviour
{
    [SerializeField] Button _btnCancel;
    [SerializeField] Button _btnConfirm;

    private void Start()
    {
        _btnCancel.onClick.AddListener(Cancel);
        _btnCancel.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));

        _btnConfirm.onClick.AddListener(Confirm);
        _btnConfirm.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    private void Cancel()
    {
        gameObject.SetActive(false);
    }

    private void Confirm()
    {
        DOTween.Clear();

        GameManager.Instance.GiveUp();
    }
}
