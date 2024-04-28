using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class RootBuyable : NetworkBehaviour
{
    [SerializeField] private int price;

    public int Price
    {
        get { return price; }
    }

    public virtual void OnBuy()
    {
        return;
    }
}
