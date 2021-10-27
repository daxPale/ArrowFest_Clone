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
    [SerializeField] private bool _desktopController = true;

    public Player player;

    void Update()
    {
        if (_desktopController)
            DesktopControl();
        else
            MobileControl();
    }
 
    public void MobileControl()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && player.IsAllive)
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

    public void DesktopControl()
    {
        if (Input.anyKeyDown && player.IsAllive && !GameManager.Instance.IsPlaying)
        {
            player.Follow();
        }
        if (Input.GetKey(KeyCode.D) && GameManager.Instance.IsPlaying)
        {
            player.Move(new Vector2(1, 0), _leftLimit, _rightLimit, _speed * 1000);
        }
        if (Input.GetKey(KeyCode.A) && GameManager.Instance.IsPlaying)
        {
            player.Move(new Vector2(-1, 0), _leftLimit, _rightLimit, _speed * 1000);
        }
    }
}
