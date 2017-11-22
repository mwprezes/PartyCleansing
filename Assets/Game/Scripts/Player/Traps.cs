using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    private GameObject thing;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.actionPoints > 0 && Input.GetKeyDown(KeyCode.E) && thing != null && !player.isHolding)
        {
            // Pierwsze "spojrzenie" do "szafy" aktywuje
            StoringItems store = thing.GetComponent<StoringItems>();
            if (!store.temperedWith && !store.storagefull)
            {
                //Trap
                thing.AddComponent<CakeTrap>();
                store.trapName = "CakeTrap";
                store.temperedWith = true;
                store.Temper("Trap");
                store.storagefull = true;
                player.actionPoints -= 1;
            }
            else if(!store.temperedWith && store.storagefull)
            {
                //Lock
                thing.AddComponent<BasicLock>();
                store.trapName = "BasicLock";
                store.temperedWith = true;
                store.Temper("Lock");
                player.actionPoints -= 1;
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Storage")
        {
            thing = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        thing = null;
    }
}
