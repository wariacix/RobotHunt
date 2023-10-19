using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicElectronicsReward : RootTechReward
{
    public override void AssignTechReward()
    {
        TechManager.Instance.sciencePointsPerWave += 5;
    }
}
