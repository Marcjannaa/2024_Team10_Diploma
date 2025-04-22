using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickup : PickupScript
{
    protected override void Collect()
    {
        if (_stats.Coins.StatValue >= 5.0)
        {
            switch (id)
            {
                case 2:
                    _stats.Bombs.Modify(1);
                    _stats.Coins.Modify(-5);
                    Destroy(gameObject);
                    break;
                case 3:
                    _stats.Keys.Modify(1);
                    _stats.Coins.Modify(-5);
                    Destroy(gameObject);
                    break;
                case 4:
                    if (_stats.Health.Value >= _stats.MaxHealth.Value)
                    {
                        break;
                    }
                    _stats.Health.Modify(10);
                    _stats.Coins.Modify(-3);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
