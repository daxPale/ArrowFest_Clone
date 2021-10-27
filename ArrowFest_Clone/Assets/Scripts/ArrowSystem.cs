using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSystem : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private float _radius = 1f;    //coefficient for calculate radius of orbits
    private int _orbit = 0;                         //amount of orbits
    private int _orbitSize = 0;                     //amount of arrows the current orbit can has
    private int _index = 1;                         //current arrow index in the orbit

    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public void AddArrows(int count)
    {
        //Instantiate arrow objects and push them to player.Arrow list
        //Cause most recently added will be given priority while being removed later
        for (int i = 0; i < count; i++)
        {
            Transform ins = Instantiate(GameManager.Instance.arrowPrefab, transform);
            ins.localPosition = SetNewArrowLocation();
            ChangeOrbit(true);
            _player.Arrows.Add(ins);
        }
        _player.UpdateScoreBoard();
    }

    public void RemoveArrows(int count)
    {
        //If there are no number of arrows to be removed, player dies
        if (_player.ArrowCount - count <= 0)
        {
            //game over
            _player.Dead();
            return;
        }

        //Arrows are removed, last added first
        for (int i = 0; i < count; i++)
        {
            Destroy(_player.Arrows[_player.ArrowCount - i - 1].gameObject);
            ChangeOrbit(false);
            _player.Arrows.RemoveAt(_player.ArrowCount - i - 1);
        }
        _player.UpdateScoreBoard();
    }

    private Vector3 SetNewArrowLocation()
    {
        //Places the arrows clockwise using the current index in the orbit
        Vector3 pos = new Vector3(0, 0, 0);
        if (_orbit == 0)
        {
            return pos;
        }
        pos.x = Mathf.Cos((360 / _orbitSize) * Mathf.PI / 180 * _index) * _orbit * _radius/100;
        pos.y = Mathf.Sin((360 / _orbitSize) * Mathf.PI / 180 * _index) * _orbit * _radius/100;
        _index++;

        return pos;
    }

    private void ChangeOrbit(bool hasAdded)
    {
        //Checks if orbit creation is needed if new arrow is added
        //Likewise, if the arrows are removed, it checks if the number of orbits is decreasing
        if (hasAdded)
        {
            if (_index > _orbitSize) 
            {
                _orbit++;
                _orbitSize = CalculateOrbitSize(_orbit);
                _index = 1;
            }
        }
        else
        {
            _index--;
            if (_index == 0)
            {
                _orbit--;
                _orbitSize = CalculateOrbitSize(_orbit);
                _index = _orbitSize;
            }
        }
    }

    private int CalculateOrbitSize(int orbit)
    {
        //Calculates how many arrows the orbit can hold
        return Mathf.RoundToInt(2 * Mathf.PI * orbit) + 1;
    }
}
