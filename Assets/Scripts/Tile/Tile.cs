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

    private void Start()
    {
        _img = GetComponent<Image>();
        gameplay = GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>();
        this.SetDefault();

        _rt = transform as RectTransform;
    }

    private void OnDisable()
    {
        DOTween.Kill(this.transform);
    }

    public void SetDefault()
    {
        _isFlipped = false;
        _isMatched = false;

        _img.sprite = _backImg;
    }

    public void Flip()
    {
        if (_isFlipped)
        {
            _rt.DOScaleX(0f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
            {
                _img.sprite = _frontImg;

                _rt.DOScaleX(1f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true);
            });
        }
        else
        {
            _rt.DOScaleX(0f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true).OnComplete(() =>
            {
                _img.sprite = _backImg;

                _rt.DOScaleX(1f, 0.15f).SetEase(Ease.InOutSine).SetUpdate(true);
            });
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isChecking || gameplay.IsChecking || _isMatched)
            return;

        _isChecking = true;

        _isFlipped = !_isFlipped;

        AudioManager.Instance.ClickedButton();

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
