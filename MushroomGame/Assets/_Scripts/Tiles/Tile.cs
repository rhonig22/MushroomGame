using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected Color _baseColor;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight, _selection;
    [SerializeField] protected bool _isWalkable;
    protected bool _isSelectable;
    protected int _xVal, _yVal;

    public BaseUnit OccupiedUnit;
    public string TileName;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public bool Selectable => _isSelectable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        _xVal = x;
        _yVal = y;
        _renderer.color = _baseColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
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

    public void SetSelectable(bool isSelectable)
    {
        _isSelectable = isSelectable;
        _selection.SetActive(isSelectable);
    }

    public Vector2 GetCoordinates()
    {
        return new Vector2(_xVal, _yVal);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        { return; }

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Player)
            {
                UnitManager.Instance.SetSelectedPlayer((PlayerUnit)OccupiedUnit);
                GridManager.Instance.ToggleSelections(true);
            }
        }
        else if (UnitManager.Instance.SelectedPlayer != null)
        {
            if (Selectable)
            {
                GridManager.Instance.ToggleSelections(false);
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);
                GameManager.Instance.ChangeState(GameState.UpdateObstacles);
            }
        }
    }
}
