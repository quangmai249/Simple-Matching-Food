using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickedOutside : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.ShowPanel(EnumPanelType.Stage);

        GameObject.FindGameObjectWithTag(TagName.BUTTON_NEXT_STAGE).GetComponent<Button>().GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag(TagName.BUTTON_NEXT_STAGE).GetComponent<Button>().interactable = true;

        GameObject.FindGameObjectWithTag(TagName.BUTTON_CANCEL_TO_HOME).GetComponent<Button>().GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag(TagName.BUTTON_CANCEL_TO_HOME).GetComponent<Button>().interactable = true;
    }
}
