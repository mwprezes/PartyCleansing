using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForState : IEnemyAI
{

    EnemyStates enemy;
    private int nextWayPoint = 1;
    private int rand = Random.Range(1, 16);
    private int wayActualNumber = 0;
    GameObject store;

    public LookForState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Patrol();
    }

    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        //enemy.navMeshAgent.Resume ();
        enemy.navMeshAgent.isStopped = false;
        //Debug.Log("#Enemy: Przeszedłem już: " + wayActualNumber);

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            if (wayActualNumber < enemy.wayAllNumber)
            {
                if (nextWayPoint == 0)
                {
                    nextWayPoint = rand;
                }
                else
                {
                    nextWayPoint = (nextWayPoint + rand) % enemy.waypoints.Length;
                    wayActualNumber++;
                }
            }
            else
            {
                nextWayPoint = 0;
            }
        }
    }


    void Watch()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, enemy.whereStorageRange))
        {
            if (hit.collider.CompareTag("Storage"))
            {
                //Debug.Log ("O, szafa");
                //enemy.searchState = hit.transform;
                //ToSearchState ();
            }
        }
    }

    public void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Storage")
        {
            Debug.Log("#Enemy: O, szafa");
            store = hit.gameObject;
            ToSearchState();
        }
        if (hit.gameObject.tag == "Pickable")
        {
            Debug.Log("#Enemy: Co to za balagan?!");
            if (hit.tag != "Busted")
            {
                PlayerController.Score = PlayerController.Score - hit.GetComponent<GrabAndDrop>().Score_for_Item;
                hit.tag = "Busted";
            }
        }
        if (hit.gameObject.tag == "Player")
        {
            //GameObject play = hit.gameObject;
            PlayerController play = hit.gameObject.GetComponent<PlayerController>();
            if (play.isHolding)
            {
                AudioSource ausrc = enemy.GetComponent<AudioSource>();
                ausrc.PlayOneShot(enemy.detected, 0.5f);
                Debug.Log("#Enemy: HEY! Co z tym robisz?");
                play.SendMessage("Bust");
            }
            else
            {
                Debug.Log("#Enemy: Witam");
            }
        }
        //Debug.Log(hit.gameObject.name);
        //Debug.Log("O, nic");
    }

    private void OnTriggerExit(Collider enemy)
    {
        store = null;
    }

    public void ToFirstWaitState()
    {
        enemy.currentState = enemy.firstWaitState;
    }

    public void ToWaitState()
    {
        enemy.currentState = enemy.waitState;
    }

    public void ToLookForState()
    {
        Debug.Log("#Enemy: 1, 2, 3 - SZUKAM!");
    }

    public void ToSearchState()
    {
        enemy.searchState.storage = store;
        enemy.currentState = enemy.searchState;
    }

    public void ToDistractState()
    {
        enemy.currentState = enemy.distractState;
    }

}
