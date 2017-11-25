using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistractState : IEnemyAI
{
    EnemyStates enemy;

    public DistractState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
      
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

    public void ToDistractState()
    {
        enemy.currentState = enemy.distractState;
    }

}