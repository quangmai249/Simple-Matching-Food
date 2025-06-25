using Assets.Scrips.Manager;
using UnityEngine;
using UnityEngine.UI;

public class PanelHUD : MonoBehaviour
{
    [SerializeField] Button _btnReplay;

    private void Awake()
    {
        _btnReplay.onClick.AddListener(Replay);
    }

    private void Replay()
    {
        StartCoroutine(GameManager.Instance.RestartGame(0, 1));
    }
}
