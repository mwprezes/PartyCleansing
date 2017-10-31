using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI  {
	void UpdateActions();
	void OnTriggerEnter(Collider hit);
	void ToWaitState();
	void ToLookForState();
	void ToSearchState();
}
