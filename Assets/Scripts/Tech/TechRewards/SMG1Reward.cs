using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEditor;

public class SMG1Reward : RootTechReward
{
    public override void AssignTechReward()
    {
        uint localPlayer = NetworkClient.localPlayer.assetId;
        ChangeBulletDamageCmd(localPlayer);
    }

    [Command]
    private void ChangeBulletDamageCmd(uint localPlayer)
    {
        ChangeBulletDamageRpc(localPlayer);
    }

    [ClientRpc]
    private void ChangeBulletDamageRpc(uint localPlayer)
    {
        for (int i = 0; i < GameManager.Instance.PlayerInstances.Count; i++)
        {
            if (GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<NetworkIdentity>().netId == localPlayer)
            {
                GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<ShootingComponent>().weapons[1].bullets[0].GetComponent<Bullet>().bulletDamage += 5;
            }
        }
    }
}