using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _offsetColor;

    public override void Init(int x, int y)
    {
        base.Init(x, y);
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = !isOffset ? _baseColor : _offsetColor;
    }
}
