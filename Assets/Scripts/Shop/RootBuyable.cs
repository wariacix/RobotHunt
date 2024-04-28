using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RootBuyable : MonoBehaviour
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
