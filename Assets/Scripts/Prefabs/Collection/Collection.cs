using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Collection : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ImageManager.Instance.ImgDetailCollection = this.GetComponent<Image>().sprite;

        UIManager.instance.ShowPanel(EnumPanelType.DetailCollection);
    }
}
