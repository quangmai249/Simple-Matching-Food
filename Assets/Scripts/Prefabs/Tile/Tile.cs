using Assets.Scrips.Manager;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private Image _img;
    private Sprite _frontImg;
    private Sprite _backImg;
    private bool _isFlipped;
    private bool _isMatched;
    private bool _isChecking;

    private RectTransform _rt;
    private Gameplay gameplay;
    private void Awake()
    {
        _img = GetComponent<Image>();
        _rt = transform as RectTransform;
        this.SetDefault();
    }
    private void Start()
    {
        gameplay = GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>();

        this.SetDefault();
    }
    private void OnDisable()
    {
        DOTween.Kill(_rt);
    }
    public void SetDefault()
    {
        _isFlipped = false;
        _isMatched = false;
        _isChecking = false;
        _img.sprite = _backImg;
        _img.color = Color.white;
    }
    public void Flip()
    {
        if (_isFlipped)
        {
            _rt.DOScaleX(0f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
            {
                _img.sprite = _frontImg;

                _rt.DOScaleX(1f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).SetAutoKill(true);
            }).SetAutoKill(true);
        }
        else
        {
            _rt.DOScaleX(0f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
            {
                _img.sprite = _backImg;

                _rt.DOScaleX(1f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).SetAutoKill(true);
            }).SetAutoKill(true);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.IsPaused || !gameplay.IsStarted || gameplay.IsWinGame)
            return;

        if (_isChecking || _isMatched || gameplay.IsChecking)
        {
            Debug.Log("Tile is already being checked or matched.");
            return;
        }

        _isChecking = true;

        _isFlipped = !_isFlipped;

        AudioManager.Instance.PlayAudioClip(EnumAudioClip.Clicked);

        this.Flip();

        GameEvents.OnTileSelected.Raise(this);
    }
    public void SetFrontImg(Sprite sprite)
    {
        _frontImg = sprite;
    }
    public void SetBackImg(Sprite sprite)
    {
        _backImg = sprite;
    }
    public Sprite GetFrontImg()
    {
        return _frontImg;
    }
    public bool IsFlipped { get => _isFlipped; set => _isFlipped = value; }
    public bool IsMatched { get => _isMatched; set => _isMatched = value; }
    public bool IsChecking { get => _isChecking; set => _isChecking = value; }
}
