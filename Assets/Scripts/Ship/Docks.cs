# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Docks : MonoBehaviour
{
    [SerializeField] private GameObject DockBlocker, Dock;
    [SerializeField] private BattleSystem battleSystem;
    public TrapdoorHealth trapdoor;
    public Wheel wheelyGood;
    public BoxCollider2D leaver; // the exit dock's IsTrigger boxcollider that lets players leave

    private void Start()
    {
        battleSystem.OnBattleStarted += BattleSystem_OnBattleStarted;
        battleSystem.OnBattleEnded += BattleSystem_OnBattleEnded;
        leaver.enabled = false; // stops players from leaving as soon as the level starts
    }

    private void BattleSystem_OnBattleStarted(object sender, System.EventArgs e)
    {
        DockBlocker.SetActive(true);
        Dock.SetActive(false);
    }

    private void BattleSystem_OnBattleEnded(object sender, System.EventArgs e)
    {
        DockBlocker.SetActive(false);
        Dock.SetActive(true);
        leaver.enabled = true; // allows players to leave when the level ends
        trapdoor.EndLevel(); // adds obols to the total and unlocks next level
        wheelyGood.LevelFinished(); // moves the boat to the bottom and stops player from moving it anymore
    }
}
