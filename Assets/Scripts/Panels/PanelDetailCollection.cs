using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDetailCollection : MonoBehaviour
{
    [SerializeField] Button btnConfirm;
    [SerializeField] Image imgCollection;

    private void Awake()
    {
        this.SetButtons();
    }

    private void OnEnable()
    {
        imgCollection.sprite = ImageManager.Instance.ImgDetailCollection;
    }

    private void SetButtons()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton);
            UIManager.instance.ShowPanel(EnumPanelType.Collections);
        });
    }
}
