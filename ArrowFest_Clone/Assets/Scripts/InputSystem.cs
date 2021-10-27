using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class InputSystem : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rightLimit = 0.4f;
    [SerializeField] private float _leftLimit = -0.4f;
    [SerializeField] private bool _tapped = false;
    public Player player;

    void Update()
    {
        MobileControl();
    }

    public void MobileControl()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //If user touches the screen, arrows start to trace spline path
            _tapped = true;
            player.Follow();
        }else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //Move player based on input touch deltaposition
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            player.Move(touchDeltaPosition, _leftLimit, _rightLimit, _speed);
        }
    }
}
