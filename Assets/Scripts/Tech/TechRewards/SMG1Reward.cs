using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG1Reward : RootTechReward
{
    public override void AssignTechReward()
    {
        GameManager.Instance.PlayerObject.GetComponent<ShootingComponent>().weapons[1].bullets[0].GetComponent<Bullet>().bulletDamage += 5;
    }
}