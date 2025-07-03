using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }

    private void OnDisable()
    {
        DOTween.Clear(true);
    }

    private void OnDestroy()
    {
    }
}
