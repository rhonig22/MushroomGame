using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossBlock : BaseObstacle
{
    [SerializeField] Sprite _deadMoss;

    public override void TakeTurn()
    {
        base.TakeTurn();
        if (_turnsAlive == _lifeSpan - 1)
        {
            GetComponent<SpriteRenderer>().sprite = _deadMoss;
        }
    }
}
