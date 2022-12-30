# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Docks : MonoBehaviour
{
    [SerializeField] private GameObject DockBlocker;
    [SerializeField] private BattleSystem battleSystem;

    private void Start()
    {
        battleSystem.OnBattleStarted += BattleSystem_OnBattleStarted;
        battleSystem.OnBattleEnded += BattleSystem_OnBattleEnded;
    }

    private void BattleSystem_OnBattleStarted(object sender, System.EventArgs e)
    {
        DockBlocker.SetActive(true);
    }

    private void BattleSystem_OnBattleEnded(object sender, System.EventArgs e)
    {
        DockBlocker.SetActive(false);
    }
}
