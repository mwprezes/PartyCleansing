using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    private GameObject thing;
    private PlayerController player;
    private PlayerControllerM playerM;

    public GameObject menu;
    public GameObject cam;
    private RadialMenu rad;
    private bool isShowing = false;
    public string trapType;

    public AudioClip a_setATrap;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
        if(player == null)
            playerM = GetComponent<PlayerControllerM>();

        menu = GameObject.Find("CanvasP");
        menu.SetActive(false);
        cam = GameObject.Find("Camera");
        rad = cam.GetComponent<RadialMenu>();
        rad.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
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
            if (thing != null)
            {
                StoringItems store = thing.GetComponent<StoringItems>();
                if (!store.temperedWith && !store.storagefull && trapType == "Cake")
                {
                    //Trap
                    player.PlaySound(a_setATrap, 1.0f);
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
                    player.PlaySound(a_setATrap, 1.0f);
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
        } else if(playerM != null)
        {
            if (thing != null)
            {
                StoringItems store = thing.GetComponent<StoringItems>();
                if (playerM.actionPoints > 0 && Input.GetKeyDown(KeyCode.E) && !store.temperedWith && !playerM.isHolding)
                {
                    //UI
                    isShowing = true;
                    menu.SetActive(true);
                    rad.enabled = true;
                }
            }
            // Pierwsze "spojrzenie" do "szafy" aktywuje
            if (thing != null)
            {
                StoringItems store = thing.GetComponent<StoringItems>();
                if (!store.temperedWith && !store.storagefull && trapType == "Cake")
                {
                    //Trap
                    playerM.PlaySound(a_setATrap, 1.0f);
                    thing.AddComponent<CakeTrap>();
                    store.trapName = "CakeTrap";
                    store.temperedWith = true;
                    store.Temper("Trap");
                    store.storagefull = true;
                    playerM.actionPoints -= 1;
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
                    playerM.PlaySound(a_setATrap, 1.0f);
                    thing.AddComponent<BasicLock>();
                    store.trapName = "BasicLock";
                    store.temperedWith = true;
                    store.Temper("Lock");
                    playerM.actionPoints -= 1;
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
