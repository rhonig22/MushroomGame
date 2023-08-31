using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _grassTile, _rockTile;

    [SerializeField] private Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
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
            }
        }

        _camera.transform.position = new Vector3((float)_width / 2 - .5f, (float)_height / 2 - .5f, -10);
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos,out var tile)) return tile;

        return null;
    }
}
