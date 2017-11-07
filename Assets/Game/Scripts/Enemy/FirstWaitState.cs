using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWaitState : IEnemyAI
{

    EnemyStates enemy;
    private float timer = 0;
    //GameObject player;
    private string message;

    public FirstWaitState(EnemyStates enemy)
    {
        this.enemy = enemy;
        ToFirstWaitState();
    }

    public void UpdateActions()
    {
        if (timer > enemy.maxTime && timer < (2 * enemy.maxTime))
        {
            enemy.patientLevel = 1;
        }

        if (timer > (2 * enemy.maxTime) && timer < (3 * enemy.maxTime))
        {
            enemy.patientLevel = 2;
        }

        if (timer > (3 * enemy.maxTime) && timer < (4 * enemy.maxTime))
        {
            enemy.patientLevel = 3;
        }

        if (timer > (4 * enemy.maxTime) && timer < (5 * enemy.maxTime))
        {
            enemy.patientLevel = 4;
        }

        if (timer > (5 * enemy.maxTime))
        {
            enemy.patientLevel = 5;
        }

        timer += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            message = "Patient level " + enemy.patientLevel;
            Debug.Log("#Enemy: 1, 2, 3 - SZUKAM! " + "[" + message + "]");
            enemy.wayAllNumber = (enemy.patientLevel + 1) * enemy.waypoints.Length / 6;
            Debug.Log("#Enemy: Do przejścia mam: " + enemy.wayAllNumber);
            //player = hit.gameObject;
            ToLookForState();
        }
    }

    private void OnTriggerExit(Collider enemy)
    {
        //player = null;
    }

    public void ToFirstWaitState()
    {
        Debug.Log("#Enemy: Ja czekam!");
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
    }

    public void ToLookForState()
    {
        enemy.currentState = enemy.lookForState;
    }

    public void ToSearchState()
    {
        Debug.Log("#Enemy: 1, 2, 3 - SZUKAM!");
        enemy.currentState = enemy.searchState;
    }
}
