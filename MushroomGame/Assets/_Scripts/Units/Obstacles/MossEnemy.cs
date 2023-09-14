using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossEnemy : BaseObstacle
{
    public override void Move()
    {
        base.Move();
        Tile currentTile = OccupiedTile;
        Tile nextTile = GetNextTile();
        if (nextTile != null && nextTile.Walkable)
        {
            nextTile.SetUnit(this);
            UnitManager.Instance.SpawnObstacleByName("MossBlock", currentTile);
        }
        else
        {
            RotateRight();
        }
    }
}
