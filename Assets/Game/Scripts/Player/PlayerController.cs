﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 18;
    private Rigidbody rig;
    public bool isHolding = false;
    Rigidbody potentialHeldObj;
    GameObject potTest;
    Rigidbody heldObj;
    private GameObject storage;
    private bool storageFull;
    private GameObject combine;

    Ray cameraRay;
    RaycastHit cameraRayHit;

    //Wskazówka
    private int Check_Weight;
    private bool showTip = false;
	private float timer = 0;
	public float tipTime = 5;
	public GUIText tipGUI;
	public TextEditor text;
    static public int Score = 0;
    public float Carried_Weight;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //Movement
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime; 

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
	    {
	            if (cameraRayHit.transform.tag == "Ground")
	            {
	                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
	                transform.LookAt(targetPosition);
	            }
	    }
        if (heldObj != null) Carried_Weight = heldObj.GetComponent<GrabAndDrop>().Weight;
            
        if (Carried_Weight <= 1) rig.MovePosition(transform.position + movement);
        else rig.MovePosition(transform.position + movement * (1 / Carried_Weight));

        // Pick up and drop
        if (isHolding)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            {
                if (storage != null && !storageFull)
                {
                    heldObj.isKinematic = false;
                    heldObj.transform.parent = null;
                    isHolding = false;
                    Debug.Log("Stored!");
                    storage.SendMessage("Store", heldObj.gameObject);
                    heldObj = null;
                    Carried_Weight = 0;
                }
                else if (combine != null) 
                {
                    
                    if (heldObj.GetComponent<GrabAndDrop>().Weight <= combine.GetComponent<Combination>().AllowedWeight)
                    {
                        heldObj.isKinematic = false;
                        heldObj.transform.parent = null;
                        isHolding = false;
                        Debug.Log("Combined!");
                        combine.SendMessage("ItemForCombination", heldObj.gameObject);
                        heldObj = null;
                    }
                    else Debug.Log("TOO HEAVY!");
                }
                else if (storage == null && combine == null)
                {
                    Debug.Log("LetGo");
                    //GameObject held = this.transform.Find("Pickable").gameObject;
                    //Rigidbody held = this.gameObject.GetComponentInChildren<Rigidbody>();
                    heldObj.isKinematic = false;
                    heldObj.transform.parent = null;
                    heldObj = null;
                    isHolding = false;
                    Carried_Weight = 0;
                }
                else
                {
                Debug.Log("Noting");
                }
                
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.F) || (Input.GetMouseButtonDown(0) && GrabAndDrop.onObj == true)) && potentialHeldObj != null)
            {
                heldObj = potentialHeldObj;
                //heldObj = potTest.GetComponent<Rigidbody>();
                pickUp(heldObj);
                isHolding = true;
                Carried_Weight = 0;
            }

            if ((Input.GetKeyDown(KeyCode.F) || (Input.GetMouseButtonDown(0) && StoringItems.onObj == true)) && storage != null)
            {
                storage.SendMessage("GiveItem", this.name);
                Carried_Weight = 0;
            }
        }

		//Wskazówka
		if(showTip) {
			if(timer < tipTime) {
				timer += Time.deltaTime;
			} else {
				tipGUI.enabled = false;
				showTip = false;
				timer = 0;
			}
		}
	}

	//Wskazówka
	void displayTipMessage(string tipText) 
	{
		tipGUI.text = tipText;
		tipGUI.enabled = true;
		this.showTip = true;
	}

    void OnGUI()
    {
        GUI.color = Color.magenta;
        GUI.Label(new Rect(10, 10, 100, 100), "Score: " + Score);
    }

    // Pick up / Store
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Pickable")
        {
			displayTipMessage("Pick me up!");
            Debug.Log("You can pick it up!");
            potentialHeldObj = hit.rigidbody;
        }
        if (hit.gameObject.tag == "Storage")
        {
            storage = hit.gameObject;
            StoringItems store = storage.GetComponent<StoringItems>();
            if(store.stored == null)
            {
                storageFull = false;
                Debug.Log("You can store stuff!");
            }
            else
            {
                storageFull = true;
                Debug.Log("This one is full!");
                //storage = null;
            }

            
        }
        /*if (hit.gameObject.tag == "Pickable" && Input.GetKeyDown(KeyCode.E) && !isHolding)
        {
            Debug.Log("Grabbed!");
            hit.gameObject.transform.parent = this.transform;
            hit.gameObject.transform.localPosition = new Vector3(0, 0, 2);
            hit.rigidbody.isKinematic = true;
            heldObj = hit.rigidbody;
            isHolding = true;
        }*/
    }

    ///
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Pickable")
        {
            displayTipMessage("Pick me up!");
            Debug.Log("You can pick it up!");
            potentialHeldObj = hit.GetComponent<Rigidbody>();
        }
        if (hit.gameObject.tag == "Storage")
        {
            storage = hit.gameObject;
            StoringItems store = storage.GetComponent<StoringItems>();
            if (store.stored == null)
            {
                storageFull = false;
                Debug.Log("You can store stuff!");
            }
            else
            {
                storageFull = true;
                Debug.Log("This one is full!");
                //storage = null;
            }
        }
        if(hit.gameObject.tag == "Combine" && isHolding)
        {
            combine = hit.gameObject;
            Debug.Log("Combination time!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        potentialHeldObj = null;
        storage = null;
        combine = null;
    }
    ///

    private void OnCollisionExit(Collision hit)
    {
        potentialHeldObj = null;
        storage = null;
    }
    private void pickUp(Rigidbody body)
    {
        Debug.Log("Grabbed!");
        body.gameObject.transform.parent = this.transform;
        body.gameObject.transform.localPosition = new Vector3(0, 0, 2);
        body.isKinematic = true;
        Carried_Weight = body.GetComponent<GrabAndDrop>().Weight;
        Debug.Log("Weight = " + Carried_Weight);
    }

    void ReciveItem(GameObject rec)
    {
        if (rec != null)
        {
            heldObj = rec.GetComponent<Rigidbody>();
            pickUp(heldObj);
            isHolding = true;
        }
    }

    void Bust()
    {
        Debug.Log("I am Busted!");
        Score = Score - heldObj.GetComponent<GrabAndDrop>().Score_for_Item;
        //GameObject held = this.transform.Find("Pickable").gameObject;
        //Rigidbody held = this.gameObject.GetComponentInChildren<Rigidbody>();
        heldObj.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj.tag = "Busted";
        heldObj = null;
        isHolding = false;
        Carried_Weight = 0;

    }
}
