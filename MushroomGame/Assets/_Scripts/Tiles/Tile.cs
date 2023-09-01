using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected Color _baseColor;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        _renderer.color = _baseColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }

        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
