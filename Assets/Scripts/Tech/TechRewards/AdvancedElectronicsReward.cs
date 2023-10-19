using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedElectronicsReward : RootTechReward
{
    public override void AssignTechReward()
    {
        TechManager.Instance.sciencePointsPerWave += 5;
    }
}