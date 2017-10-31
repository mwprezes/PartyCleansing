using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour {

	public Transform[] waypoints;
	public int whereStorageRange;
	public int maxTime;

    [HideInInspector]
    public int wayAllNumber;
    [HideInInspector]
    public int patientLevel = 0;
	[HideInInspector] 
	public WaitState waitState;
	[HideInInspector]
	public LookForState lookForState;
	[HideInInspector]
	public SearchState searchState;
	[HideInInspector]
	public IEnemyAI currentState;
	[HideInInspector]
	public NavMeshAgent navMeshAgent;
	[HideInInspector]


	void Awake(){
		waitState = new WaitState (this);
		lookForState = new LookForState (this);
		searchState = new SearchState (this);
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}


	// Use this for initialization
	void Start () {
		currentState = waitState;
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateActions ();
	}

	void OnTriggerEnter (Collider otherObj){
		currentState.OnTriggerEnter(otherObj);
	}

    private void pickUp(GameObject body)
    {
        Debug.Log("#Enemy: Woosh!");
        body.gameObject.transform.parent = this.transform;
        body.gameObject.transform.localPosition = new Vector3(0, 0, 2);
        body.GetComponent<Rigidbody>().isKinematic = true;

        body.gameObject.transform.parent = null;
        body.GetComponent<Rigidbody>().isKinematic = false;
        PlayerController.Score = PlayerController.Score - body.GetComponent<GrabAndDrop>().Score_for_Item;
        if (body.tag != "Busted")
        {
            body.GetComponent<GrabAndDrop>().Score_for_Item /= 2;
            body.tag = "Busted";
        }
    }

    public void ReciveItem(GameObject rec)
    {
        if (rec != null)
        {
            Debug.Log("#Enemy: Znaleziony");
            pickUp(rec);

        }
        else
        {
            Debug.Log("#Enemy: Pusto");
        }
    }
}
