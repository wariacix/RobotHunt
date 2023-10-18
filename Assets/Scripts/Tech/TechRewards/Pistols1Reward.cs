using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols1Reward : RootTechReward
{
    public override void AssignTechReward()
    {
        GameManager.Instance.LocalPlayerObject.GetComponent<ShootingComponent>().weapons[0].bullets[0].GetComponent<Bullet>().bulletDamage += 5;
    }
}