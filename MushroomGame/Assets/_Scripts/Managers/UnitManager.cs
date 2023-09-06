using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;

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

        GameManager.Instance.ChangeState(GameState.SpawnObstacles);
    }

    public void SpawnObstacle()
    {
        var obstacleCount = 1;
        for (int i = 0; i < obstacleCount; i++)
        {
            var obstaclePrefab = GetRandomUnit<BaseObstacle>(Faction.Obstacle);
            var obstacle = Instantiate(obstaclePrefab);
            var spawnTile = GridManager.Instance.GetObstacleSpawn();
            spawnTile.SetUnit(obstacle);
        }

        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }

    public void SetSelectedPlayer(PlayerUnit player)
    {
        SelectedPlayer = player;
        MenuManager.Instance.ShowSelectedUnit(SelectedPlayer);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u=>u.Faction == faction).OrderBy(o => Random.value).FirstOrDefault().UnitPrefab;
    }
}
