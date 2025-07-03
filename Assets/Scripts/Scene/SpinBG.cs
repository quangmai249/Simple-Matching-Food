using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBG : MonoBehaviour
{
    [SerializeField] float spinSpeed = 1f;
    [SerializeField] float zoomSpeed = 0.01f;

    private bool _isScaleX = false;
    private float _valScaleDf;
    private float _maxvalScaleDf;

    private GameObject _spinBG;

    private void Awake()
    {
        _spinBG = GameObject.FindGameObjectWithTag(TagName.TAG_BACKGROUND);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void OnDestroy()
    {
    }

    private void Start()
    {
        _valScaleDf = _spinBG.transform.localScale.x;
        _maxvalScaleDf = _valScaleDf * 2;
    }

    private void Update()
    {
        this.Spin();
        this.Zoom();
    }

    private void Spin()
    {
        _spinBG.transform.Rotate(new Vector3(0, 0, Time.deltaTime * spinSpeed));
    }

    private void Zoom()
    {
        if (_spinBG.transform.localScale.x < _valScaleDf)
            _isScaleX = true;
        else if (_spinBG.transform.localScale.x > _maxvalScaleDf)
            _isScaleX = false;

        if (_isScaleX)
            _spinBG.transform.localScale += Vector3.one * Time.deltaTime * zoomSpeed;
        else
            _spinBG.transform.localScale -= Vector3.one * Time.deltaTime * zoomSpeed;
    }
}
