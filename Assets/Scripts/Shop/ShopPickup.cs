using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickup : PickupScript
{
    protected override void Collect()
    {
        if (Player_Stats.Coins.StatValue >= 5.0)
        {
            switch (id)
            {
                case 2:
                    Player_Stats.Bombs.Modify(1);
                    Player_Stats.Coins.Modify(-5);
                    StartCoroutine(CollectAndShrink());
                    break;
                case 3:
                    Player_Stats.Keys.Modify(1);
                    Player_Stats.Coins.Modify(-5);
                    StartCoroutine(CollectAndShrink());
                    break;
                case 4:
                    Player_Stats.Health.Modify(10);
                    Player_Stats.Coins.Modify(-3);
                    StartCoroutine(CollectAndShrink());
                    break;
            }
        } else if (Player_Stats.Coins.StatValue >= 3.0)
        {
            switch (id)
            {
                case 4:
                    Player_Stats.Health.Modify(10);
                    Player_Stats.Coins.Modify(-3);
                    StartCoroutine(CollectAndShrink());
                    break;
            }
        }
    }
}
