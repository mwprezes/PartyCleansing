using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    private GameObject thing;
    private PlayerController player;

    public GameObject menu;
    public GameObject cam;
    private RadialMenu rad;
    private bool isShowing = false;
    public string trapType;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
        menu.SetActive(false);
        rad = cam.GetComponent<RadialMenu>();
        rad.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (thing != null)
        {
            StoringItems store = thing.GetComponent<StoringItems>();
            if (player.actionPoints > 0 && Input.GetKeyDown(KeyCode.E) && !store.temperedWith && !player.isHolding)
            {
                //UI
                isShowing = true;
                menu.SetActive(true);
                rad.enabled = true;
            }
        }
        // Pierwsze "spojrzenie" do "szafy" aktywuje
        if (thing != null) {
            StoringItems store = thing.GetComponent<StoringItems>();
            if (!store.temperedWith && !store.storagefull && trapType == "Cake")
            {
                //Trap
                thing.AddComponent<CakeTrap>();
                store.trapName = "CakeTrap";
                store.temperedWith = true;
                store.Temper("Trap");
                store.storagefull = true;
                player.actionPoints -= 1;
                if (isShowing)
                {
                    isShowing = !isShowing;
                    menu.SetActive(false);
                    rad.enabled = false;
                }
                trapType = null;
            }
            else if (!store.temperedWith && trapType == "BasicLock")
            {
                //Lock
                thing.AddComponent<BasicLock>();
                store.trapName = "BasicLock";
                store.temperedWith = true;
                store.Temper("Lock");
                player.actionPoints -= 1;
                if (isShowing)
                {
                    isShowing = !isShowing;
                    menu.SetActive(false);
                    rad.enabled = false;
                }
                trapType = null;
            }
        }
        else
        {
            if (isShowing)
            {
                isShowing = !isShowing;
                menu.SetActive(false);
                rad.enabled = false;
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
