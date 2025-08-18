using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCollection : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;

    private Vector2 _lastTouchPos;
    private Vector3 _rotationDefault;

    private void Start()
    {
        _rotationDefault = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                _lastTouchPos = touch.position;
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - _lastTouchPos;
                this.transform.Rotate(0f, 0f, delta.x * speed);
                _lastTouchPos = touch.position;
            }
        }
    }

    private void OnEnable()
    {
        this.transform.rotation = Quaternion.Euler(_rotationDefault);
    }
}
