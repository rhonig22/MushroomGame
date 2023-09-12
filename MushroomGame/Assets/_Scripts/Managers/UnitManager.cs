using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    private List<BaseObstacle> _obstacles = new List<BaseObstacle>();
    private List<BaseObstacle> _pendingObstacles = new List<BaseObstacle>();
    private List<BaseObstacle> _toRemove = new List<BaseObstacle>();
    private bool _updatingObstacles = false;

    public PlayerUnit SelectedPlayer;

    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("units").ToList();
    }

    public void SpawnPlayer()
    {
        var playerPrefab = GetRandomUnit<PlayerUnit>(Faction.Player);
        var player = Instantiate(playerPrefab);
        var spawnTile = GridManager.Instance.GetPlayerTile();
        spawnTile.SetUnit(player);
    }

    public void SpawnObstacles()
    {
        var obstacleCount = 1;
        for (int i = 0; i < obstacleCount; i++)
        {
            var spawnTile = GridManager.Instance.GetObstacleSpawn();
            SpawnObstacleByName("Moss", spawnTile);
        }
    }

    public void SpawnObstacleByName(string name, Tile tile)
    {
        var obstaclePrefab = GetUnitByName<BaseObstacle>(name);
        var obstacle = Instantiate(obstaclePrefab);
        tile.SetUnit(obstacle);
        AddObstacle(obstacle);
    }

    public void UpdateObstacles()
    {
        _updatingObstacles = true;
        foreach (BaseObstacle unit in _obstacles)
        {
            unit.TakeTurn();
            if (unit.ShouldDie())
            {
                unit.Die();
                _toRemove.Add(unit);
            }
        }

        _updatingObstacles = false;
        if (_toRemove.Count > 0)
        {
            foreach (BaseObstacle unit in _toRemove)
            {
                _obstacles.Remove(unit);
                Destroy(unit.gameObject);
            }

            _toRemove.Clear();
        }

        if (_pendingObstacles.Count > 0)
        {
            _obstacles.AddRange(_pendingObstacles);
            _pendingObstacles.Clear();
        }
    }

    public void SetSelectedPlayer(PlayerUnit player)
    {
        SelectedPlayer = player;
        MenuManager.Instance.ShowSelectedUnit(SelectedPlayer);
    }

    private void AddObstacle(BaseObstacle unit)
    {
        if (_updatingObstacles)
        {
            _pendingObstacles.Add(unit);
        }
        else
        {
            _obstacles.Add(unit);
        }
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u=>u.Faction == faction).OrderBy(o => Random.value).FirstOrDefault().UnitPrefab;
    }

    private T GetUnitByName<T>(string name) where T : BaseUnit
    {
        return (T)_units.Where(u => u.UnitName == name).OrderBy(o => Random.value).FirstOrDefault().UnitPrefab;
    }
}
