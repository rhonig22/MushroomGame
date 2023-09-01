using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
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

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u=>u.Faction == faction).OrderBy(o => Random.value).FirstOrDefault().UnitPrefab;
    }
}
