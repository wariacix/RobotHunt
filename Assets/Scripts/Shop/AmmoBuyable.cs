using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBuyable : RootBuyable
{
    [SerializeField] private int ammoAmount;
    [SerializeField] private int id;

    public int AmmoAmount
    {
        get { return ammoAmount; }
    }

    public override void OnBuy()
    {
        GameManager.Instance.PlayerObject.GetComponent<ShootingComponent>();
    }
}
