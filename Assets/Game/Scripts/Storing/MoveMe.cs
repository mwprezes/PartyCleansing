using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour {

    public float weight = 2;
    private Rigidbody rig;
    private GameObject[] child;
    public bool isMoved = false;

    // Use this for initialization
    void Start ()
    {
        rig = this.gameObject.GetComponent<Rigidbody>();
        //rig.isKinematic = true;
        //child = this.GetComponentsInChildren<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
