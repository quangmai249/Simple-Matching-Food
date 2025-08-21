using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collection : MonoBehaviour, IPointerClickHandler
{
    private int _isClicked;

    private void Start()
    {
        _isClicked = PlayerPrefs.GetInt(this.GetComponent<Image>().sprite.name, 0);
        this.SetNotifySelected(_isClicked);
    }

    private void SetNotifySelected(int check)
    {
        if (_isClicked == 1)
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        else
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked == 0)
        {
            _isClicked = 1;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            PlayerPrefs.SetInt(this.GetComponent<Image>().sprite.name, _isClicked);
            PlayerPrefs.Save();
        }

        ImageManager.Instance.ImgDetailCollection = this.GetComponent<Image>().sprite;

        AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton);

        UIManager.instance.ShowPanel(EnumPanelType.DetailCollection);
    }
}
