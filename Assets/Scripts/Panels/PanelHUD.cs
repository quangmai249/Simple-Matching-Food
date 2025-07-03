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
        GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>().CountArr = 0;

        GameManager.Instance.DisableAllTiles();
        GameManager.Instance.RestartGame(1f);
    }
}
