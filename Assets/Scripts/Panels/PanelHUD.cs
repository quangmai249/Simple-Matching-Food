using Assets.Scrips.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelHUD : MonoBehaviour
{
    [SerializeField] Button _btnPause;
    [SerializeField] Button _btnResume;

    [SerializeField] GameObject panelPause;

    private void Awake()
    {
        this.SetButtons();
        this.SetActiveButtons(false);
    }

    private void Start()
    {
        panelPause.SetActive(false);
    }

    private void SetButtons()
    {
        _btnPause.onClick.AddListener(Pause);
        _btnResume.onClick.AddListener(Resume);

        _btnPause.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnResume.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
    }

    private void Pause()
    {
        GameManager.Instance.IsPaused = true;
        GameManager.Instance.PauseGame();
        this.SetActiveButtons(true);

        panelPause.gameObject.SetActive(true);
    }

    private void Resume()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.ResumeGame();
        this.SetActiveButtons(false);

        panelPause.gameObject.SetActive(false);
    }

    private void SetActiveButtons(bool isPause)
    {
        _btnPause.gameObject.SetActive(!isPause);
        _btnResume.gameObject.SetActive(isPause);
    }
}
