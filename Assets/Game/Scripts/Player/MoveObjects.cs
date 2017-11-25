using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour {

    private GameObject potentialToBeMoved;
    private GameObject toBeMoved;
    private GameObject parent;
    private bool isGrabbing = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGrabbing)
        {
            if (Input.GetKeyDown(KeyCode.F) && potentialToBeMoved != null)
            {
                Grab();
            }
        }
        else
        {
            if (toBeMoved != null)
            {
                if (Vector3.Distance(this.gameObject.transform.position, parent.transform.position) > 2.0f)
                {
                    //Debug.Log("Aaaaand I raaaaaan, I raaaan so far awaaaaaayyy");
                    Quaternion lookRot;
                    Vector3 lookDir;

                    PlayerController player = this.GetComponent<PlayerController>();
                    //parent.GetComponent<Rigidbody>().AddForce(player.movement);
                    MoveMe script = parent.GetComponent<MoveMe>();
                    float speed = (script.weight + 4.0f) * Time.deltaTime;

                    lookDir = (this.transform.position - parent.transform.position).normalized;
                    lookRot = Quaternion.LookRotation(lookDir);

                    toBeMoved.transform.position = Vector3.MoveTowards(toBeMoved.transform.position, this.transform.position, speed);
                    parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRot, speed / 2);
                    if (Vector3.Distance(toBeMoved.transform.position, parent.transform.position) > 5.0f)
                    {
                        parent.transform.position = Vector3.MoveTowards(parent.transform.position, toBeMoved.transform.position, speed);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerController player = this.GetComponent<PlayerController>();

                toBeMoved.transform.localPosition = new Vector3(0.0f, 0.0f, -1.5f);
                parent.transform.parent = null;
                toBeMoved.transform.parent = null;

                parent.GetComponent<Rigidbody>().isKinematic = true;
                parent.transform.parent = toBeMoved.transform;
                //parent.transform.localPosition = (toBeMoved.transform.position - parent.transform.position).normalized;

                player.Carried_Weight = 0;
                toBeMoved = null;
                parent = null;
                isGrabbing = false;
            }
        }
	}

    private void OnTriggerStay(Collider hit)
    {
        if(hit.tag == "GrabNode")
        {
            //Debug.Log("Hello?");
            potentialToBeMoved = hit.gameObject;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        potentialToBeMoved = null;
    }

    void Grab()
    {
        Debug.Log("Push/Pull");
        toBeMoved = potentialToBeMoved;

        Rigidbody[] par = toBeMoved.GetComponentsInChildren<Rigidbody>();
        parent = par[1].gameObject;
        //Rigidbody rigp = par[1].GetComponent<Rigidbody>();
        MoveMe moveme = par[1].GetComponent<MoveMe>();
        moveme.isMoved = true;

        par[1].isKinematic = false;
        parent.transform.parent = null;
        //toBeMoved.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        toBeMoved.transform.parent = this.transform;
        toBeMoved.gameObject.transform.localPosition = new Vector3(0, 0, 2);
        //par[1].gameObject.transform.parent = toBeMoved.transform;

        PlayerController player = this.GetComponent<PlayerController>();
        player.Carried_Weight = moveme.weight;
        //player.canRotate = false;

        //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        isGrabbing = true;
    }
}
