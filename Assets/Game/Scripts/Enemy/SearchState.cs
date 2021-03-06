﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IEnemyAI {

	EnemyStates enemy;
	public GameObject storage;

	public SearchState(EnemyStates enemy)
	{
		this.enemy = enemy;
	}

	public void UpdateActions(){
		enemy.navMeshAgent.isStopped = true;
		Search ();
		//ToWaitState ();
		ToLookForState();
	}

	public void Search(){
		if(storage != null)
        {
            Debug.Log("#Enemy: Zobaczmy co jest w srodku");
			storage.SendMessage ("GiveItem", this.enemy.name);
        }
	}

	public void OnTriggerEnter(Collider enemy){

	}

	public void ToWaitState(){
		enemy.currentState = enemy.waitState;
	}

	public void ToLookForState(){
		enemy.currentState = enemy.lookForState;
	}

	public void ToSearchState(){
		Debug.Log ("#Enemy: A co my tu mamy?");
	}
}
