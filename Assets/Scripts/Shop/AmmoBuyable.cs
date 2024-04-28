using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

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
        NetworkClient.localPlayer.gameObject.GetComponent<ShootingComponent>();
    }
}
