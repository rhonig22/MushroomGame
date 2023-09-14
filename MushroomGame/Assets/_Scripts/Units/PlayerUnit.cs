using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    protected override void Update()
    {
        base.Update();
        if (OccupiedTile.Collectible != null)
        {
            var collectible = OccupiedTile.Collectible;
            PlayerController.Instance.AddCollectible(collectible);
            OccupiedTile.Collectible = null;
            Destroy(collectible.gameObject);
        }

    }
}
