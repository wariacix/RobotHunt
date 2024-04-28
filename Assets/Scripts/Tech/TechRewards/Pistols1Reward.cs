using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistols1Reward : RootTechReward
{
    [SerializeField] private GameObject upgradedBullet;
    public override void AssignTechReward()
    {
        uint localPlayer = NetworkClient.localPlayer.assetId;
        ChangeBulletDamageCmd(localPlayer);
    }

    [Command]
    private void ChangeBulletDamageCmd(uint localPlayer)
    {
        ChangeBulletDamageRpc(localPlayer);
        NetworkManager.singleton.spawnPrefabs.Add(upgradedBullet);
        for (int i = 0; i < GameManager.Instance.PlayerInstances.Count; i++)
        {
            if (GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<NetworkIdentity>().netId == localPlayer)
            {
                GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<ShootingComponent>().weapons[0].bullets[0] = upgradedBullet;
            }
        }
    }

    [ClientRpc]
    private void ChangeBulletDamageRpc(uint localPlayer)
    {
        for (int i = 0; i < GameManager.Instance.PlayerInstances.Count; i++)
        {
            if (GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<NetworkIdentity>().netId == localPlayer)
            {
                GameManager.Instance.PlayerInstances[i].gameObject.GetComponent<ShootingComponent>().weapons[0].bullets[0] = upgradedBullet;
            }
        }
    }
}