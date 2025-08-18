using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDetailCollection : MonoBehaviour
{
    [SerializeField] Button btnConfirm;
    [SerializeField] Image imgCollection;
    [SerializeField] TextMeshProUGUI txtNameCollection;
    [SerializeField] ReadCollection _readCollection;

    private void Awake()
    {
        this.SetButtons();
    }

    private void OnEnable()
    {
        imgCollection.sprite = ImageManager.Instance.ImgDetailCollection;

        string s = _readCollection.GetValue(imgCollection.sprite.name);

        if (s != string.Empty)
            txtNameCollection.text = s;
        else
            txtNameCollection.text = imgCollection.sprite.name;
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
