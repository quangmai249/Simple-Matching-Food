using Assets.Scrips.Manager;
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
}
