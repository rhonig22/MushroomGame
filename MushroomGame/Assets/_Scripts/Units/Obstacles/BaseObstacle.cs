using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : BaseUnit
{
    protected int _turnsAlive = 0;
    [SerializeField] protected int _lifeSpan = 1, _speed = 0;
    [SerializeField] protected bool _canDie = false;
    [SerializeField] protected Direction _direction = Direction.Left;

    public bool ShouldDie()
    {
        if (_canDie && _turnsAlive >= _lifeSpan)
        {
            return true;
        }

        return false;
    }

    public virtual void TakeTurn()
    {
        _turnsAlive++;
        Move();
    }

    public virtual void Move()
    {
    }

    public virtual void Die()
    {
        OccupiedTile.OccupiedUnit = null;
        OccupiedTile = null;
    }

    protected Tile GetNextTile()
    {
        Vector2 pos = OccupiedTile.GetCoordinates();
        switch (_direction)
        {
            case Direction.Left:
                pos.x -= _speed;
                break;
            case Direction.Right:
                pos.x += _speed;
                break;
            case Direction.Up:
                pos.y += _speed;
                break;
            case Direction.Down:
                pos.y -= _speed;
                break;
            default:
                break;
        }

        return GridManager.Instance.GetTileAtPos(pos);
    }

    protected void RotateRight()
    {
        _direction = (Direction)(((int)_direction + 1) % 4);
        transform.Rotate(Vector3.forward, -90);
    }

    protected void RotateLeft()
    {
        _direction = (Direction)(((int)_direction - 1) % 4);
        transform.Rotate(Vector3.forward, 90);
    }

    protected void Flip()
    {
        _direction = (Direction)(((int)_direction + 2) % 4);
        transform.Rotate(Vector3.forward, 180);
    }
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
