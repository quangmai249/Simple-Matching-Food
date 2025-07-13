using Assets.Scrips.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelHUD : MonoBehaviour
{
    [SerializeField] Button _btnPause;
    [SerializeField] Button _btnResume;

    private Gameplay gameplay;
    private void Awake()
    {
        _btnPause.onClick.AddListener(Pause);
        _btnResume.onClick.AddListener(Resume);

        _btnPause.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));
        _btnResume.onClick.AddListener(() => AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton));

        gameplay = GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>();
    }

    private void Start()
    {
        this.SetDefault(false);
    }

    private void Update()
    {

    }

    private void SetDefault(bool isPause)
    {
        _btnPause.gameObject.SetActive(!isPause);
        _btnResume.gameObject.SetActive(isPause);
    }

    private void Pause()
    {
        GameManager.Instance.IsPaused = true;
        GameManager.Instance.PauseGame();
        this.SetDefault(true);
    }

    private void Resume()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.ResumeGame();
        this.SetDefault(false);
    }
}
