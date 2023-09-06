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
    public string TileName;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
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

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        { return; }

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Player)
            {
                UnitManager.Instance.SetSelectedPlayer((PlayerUnit)OccupiedUnit);
            }
            else if (UnitManager.Instance.SelectedPlayer != null)
            {
                UnitManager.Instance.SetSelectedPlayer(null);
            }
        }
        else if (UnitManager.Instance.SelectedPlayer != null)
        {
            SetUnit(UnitManager.Instance.SelectedPlayer);
            UnitManager.Instance.SetSelectedPlayer(null);
        }
    }
}
