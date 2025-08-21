using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelDetailCollection : MonoBehaviour
{
    [SerializeField] Button btnConfirm;
    [SerializeField] Image imgCollection;
    [SerializeField] TextMeshProUGUI txtNameCollection;
    [SerializeField] TextMeshProUGUI txtDescriptionCollection;
    [SerializeField] ReadCollection _readCollection;

    private void Awake()
    {
        this.SetButtons();
    }

    private void OnEnable()
    {
        imgCollection.sprite = ImageManager.Instance.ImgDetailCollection;

        int index = _readCollection.GetIndex(imgCollection.sprite.name);

        string s = _readCollection.GetNameCollection(index);

        txtNameCollection.text = _readCollection.GetNameCollection(index);
        txtDescriptionCollection.text = _readCollection.GetDescriptionCollection(index);
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
