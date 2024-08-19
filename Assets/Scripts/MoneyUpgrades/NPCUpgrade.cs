using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recreational Upgrade/ NPC Count", fileName = "NPC Count")]

public class NPCUpgrade : UpgradeCreator
{
    public override void ApplyUpgrade()
    {
        GameManager.instance.npcCount += upgradeAmount;
    }
}
