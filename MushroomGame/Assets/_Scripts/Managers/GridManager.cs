using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _grassTile, _rockTile;

    [SerializeField] private Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;
    private GameObject _grid;

    private void Awake()
    {
        Instance = this;
        _grid = new GameObject("Grid");
    }

    public void GenerateGrid()
    {
        if (_tiles == null)
            _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tilePrefab = x == 0 || y == 0 || x == _width - 1 || y == _height - 1 ? _rockTile : _grassTile;
                var tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                tile.name = $"Tile {x} {y}";

                tile.Init(x,y);
                _tiles[new Vector2(x, y)] = tile;
                tile.transform.parent = _grid.transform;
            }
        }

        _camera.transform.position = new Vector3((float)_width / 2 - .5f, (float)_height / 2 - .5f, -10);
    }

    public void ClearGrid()
    {
        if ( _tiles != null && _tiles.Count > 0)
        {
            _tiles.Clear();
            DestroyImmediate(_grid);
            _grid = new GameObject("Grid");
        }
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos,out var tile)) return tile;

        return null;
    }

    public Tile GetPlayerTile()
    {
        return _tiles.Where(t => t.Key.x < _width && t.Value.Walkable).FirstOrDefault().Value;
    }

    public Tile GetObstacleSpawn()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(o => Random.value).FirstOrDefault().Value;
    }

    public Tile GetRandomSpawn()
    {
        return _tiles.Where(t => t.Value.Walkable).OrderBy(o => Random.value).FirstOrDefault().Value;
    }

    public void ToggleSelections(bool showSelections)
    {
        var player = UnitManager.Instance.SelectedPlayer.OccupiedTile;
        var playerCoords = player.GetCoordinates();
        SetTileSelection(new Vector2(playerCoords.x - 1, playerCoords.y), showSelections);
        SetTileSelection(new Vector2(playerCoords.x + 1, playerCoords.y), showSelections);
        SetTileSelection(new Vector2(playerCoords.x, playerCoords.y - 1), showSelections);
        SetTileSelection(new Vector2(playerCoords.x, playerCoords.y + 1), showSelections);
    }

    private void SetTileSelection(Vector2 coord, bool showSelection)
    {
        if (coord.x >= 0 && coord.x < _width &&
            coord.y >= 0 && coord.y < _height)
        {
            var tile = _tiles[coord];
            if (tile.Walkable)
            {
                tile.SetSelectable(showSelection);
            }
        }
    }
}
