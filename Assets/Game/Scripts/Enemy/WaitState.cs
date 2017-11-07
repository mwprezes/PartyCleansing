using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IEnemyAI
{

    EnemyStates enemy;
    private float timer = 0;

    public WaitState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        if (timer > (enemy.maxTime / 2))
        {
            ToLookForState();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToFirstWaitState()
    {
        enemy.currentState = enemy.firstWaitState;
    }

    public void ToWaitState()
    {
        Debug.Log("#Enemy: Yhmmm...");
    }

    public void ToLookForState()
    {
        enemy.currentState = enemy.lookForState;
    }

    public void ToSearchState()
    {
        enemy.currentState = enemy.waitState;
    }
}