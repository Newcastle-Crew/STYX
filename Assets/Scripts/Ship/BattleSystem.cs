#region 'Using' information
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using this tutorial: https://youtu.be/gbFBWxtpgpQ

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    [SerializeField] private ColliderTrigger colliderTrigger; // used to spawn the waves through the player standing in a specific box

    public event EventHandler OnBattleStarted; // when level starts, stop 'em leaving the boat
    public event EventHandler OnBattleEnded; // when level ends, let 'em leave the boat

    private State state;

    private enum State
    {
        Idle, // before battle is activated
        Active, // while battle is activated
        BattleOver, // when level is finished
    }

    private void Awake() // starts the game off in the chill mode
    {
        state = State.Idle;
    }

    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle) // only starts battle mode once
        {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
    }

    private void StartBattle()
    {
        OnBattleStarted?.Invoke(this, EventArgs.Empty);
        state = State.Active;
    }

    private void Update() {
        switch (state) {
            case State.Active:
                foreach (Wave wave in waveArray) { 
                    wave.Update();
                    if (wave.IsWaveOver()) continue; // won't spawn the next lot of enemies unless the current geezers are dead
                    else break;
                }
                TestBattleOver();
                break;
        }
    }

    private void TestBattleOver()
    {
        if(state == State.Active)
        { 
            if(AreWavesOver()) // all waves killed, level is complete
            {
                state = State.BattleOver;
                OnBattleEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool AreWavesOver()
    {
        foreach (Wave wave in waveArray)
        {
            if(wave.IsWaveOver())
            {
                // wave is over
            }
            else
            { return false; }
        }
        return true;
    }





    [System.Serializable] private class Wave     // represents a single wave of enemy spawns
    {
        [SerializeField] private Health[] enemySpawnArray; // used to spawn the waves through unity hierarchy magic
        [SerializeField] private float timer;

        private void SpawnEnemies()
        {
            foreach (Health enemySpawn in enemySpawnArray)
            { enemySpawn.Spawn(); }
        }

        public void Update()
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                { SpawnEnemies(); }
            }
        }

        public bool IsWaveOver()
        {
            if (timer < 0)
            {
                foreach (Health enemySpawn in enemySpawnArray)
                {
                    if (enemySpawn.IsAlive())
                    {
                        return false;
                    }
                }
                return true;
            }
            else // enemies not spawned yet
            { return false; }
        }
    }
}
