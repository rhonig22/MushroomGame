using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public Faction Faction;
    public string UnitName;

    private float _lerpSpeed = .5f, _current, _target, _goalScale = 1f;
    private bool _initialPositionSet = false;
    private Vector3 _goalPosition;

    protected virtual void Update()
    {
        _current = Mathf.MoveTowards(_current, _target, _lerpSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _goalPosition, _current);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * _goalScale, _current);
    }

    public void SetGoalPosition(Vector3 goalPosition)
    {
        if (!_initialPositionSet)
        {
            transform.localScale = Vector3.zero;
            transform.position = goalPosition;
            _goalPosition = goalPosition;
            _initialPositionSet = true;
            _current = 0;
            _target = _lerpSpeed;
        }
        else
        {
            _goalPosition = goalPosition;
            _current = 0;
            _target = _lerpSpeed;
        }
    }
}
