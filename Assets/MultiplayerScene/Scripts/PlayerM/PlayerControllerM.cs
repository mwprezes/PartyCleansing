﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerM : MonoBehaviour
{
    private float waitTime = 0;
    private float maxTime = 5;

    public float speed = 50;
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

    //Pkt akcji
    public int actionPoints = 2;

    //Wskazówka
    private int Check_Weight;
    private bool showTip = false;
    private float timer = 0;
    public float tipTime = 5;
    public GUIText tipGUI;
    public TextEditor text;
    static public int Score = 0;
    public float Carried_Weight;

    // Olsza's HINTS 
    public bool HintShow = false;
    private string HintText = "";

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();

        HintShow = true;
        HintText = "Quick. I gotta clean this up! ";
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
        Vector3 rot = new Vector3(hAxis, 0, vAxis);
        if (rot != new Vector3(0, 0, 0))
            transform.rotation = Quaternion.LookRotation(rot);

        if (heldObj != null) Carried_Weight = heldObj.GetComponent<GrabAndDrop>().Weight;

        if (Carried_Weight <= 1) rig.MovePosition(transform.position + movement);
        else rig.MovePosition(transform.position + movement * (1 / Carried_Weight));

        rig.MoveRotation(transform.rotation);

        //transform.Translate(movement/* * speed * Time.deltaTime*/, Space.World);

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

                    HintText = "Uff, it's hiden!";
                    StartCoroutine(Wait());

                    storage.SendMessage("Store", heldObj.gameObject);
                    heldObj = null;
                    //potentialHeldObj = null;
                    Carried_Weight = 0;
                }
                else if (combine != null)
                {

                    if (heldObj.GetComponent<GrabAndDrop>().Weight <= combine.GetComponent<Combination>().AllowedWeight)
                    {
                        heldObj.isKinematic = false;
                        heldObj.transform.parent = null;
                        isHolding = false;
                        //WaitASecond("Combined");
                        
                        Debug.Log("Combined!");

                        HintText = "Yey, combined!";
                        StartCoroutine(Wait());

                        combine.SendMessage("ItemForCombination", heldObj.gameObject);
                        heldObj = null;
                        //potentialHeldObj = null;
                        Carried_Weight = 0;
                    }
                    else
                    {
                        Debug.Log("TOO HEAVY!");
                        HintText = "Nope, It's too heavy!";
                        StartCoroutine(Wait());
                    }
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
                    //potentialHeldObj = null;
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
            if ((Input.GetKeyDown(KeyCode.F)/* || (Input.GetMouseButtonDown(0) && GrabAndDrop.onObj == true)*/) && potentialHeldObj != null)
            {
                heldObj = potentialHeldObj;
                //heldObj = potTest.GetComponent<Rigidbody>();
                pickUp(heldObj);
                isHolding = true;
                //Carried_Weight = 0;
            }

            if ((Input.GetKeyDown(KeyCode.F)/* || (Input.GetMouseButtonDown(0) && StoringItems.onObj == true)*/) && storage != null)
            {
                storage.SendMessage("GiveItem", this.name);
                //Carried_Weight = 0;
            }
        }

        //Wskazówka
        if (showTip)
        {
            if (timer < tipTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
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

    void WaitASecond(string textMessage)
    {
        float tempspeed;
        tempspeed = speed;

        Debug.Log(textMessage);
        while (waitTime<maxTime)
        {
            speed = 0;
            //Debug.Log(textMessage);
            waitTime += Time.deltaTime;
        }

        speed = tempspeed;
        if (waitTime > maxTime)
        {
            waitTime = 0;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        HintText = "";
    }


    void OnGUI()
    {
        GUI.color = Color.magenta;
        GUI.Label(new Rect(10, 10, 100, 100), "Score: " + Score);

        if (HintShow)
        {
            GUI.color = Color.white;
            var HintPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            GUI.Label(new Rect(HintPosition.x - 20, Screen.height - HintPosition.y - 70, 250, 25), "<size=18>" + HintText + "</size>");
        }

    }

    // Pick up / Store
    // private void OnCollisionEnter(Collision hit)
    // {
    //     if (hit.gameObject.tag == "Pickable")
    //     {
    //displayTipMessage("Pick me up!");
    //         Debug.Log("You can pick it up!");
    //         potentialHeldObj = hit.rigidbody;
    //     }
    //     if (hit.gameObject.tag == "Storage")
    //     {
    //         storage = hit.gameObject;
    //         StoringItems store = storage.GetComponent<StoringItems>();
    //         if(store.stored == null)
    //         {
    //             storageFull = false;
    //             Debug.Log("You can store stuff!");
    //         }
    //         else
    //         {
    //             storageFull = true;
    //             Debug.Log("This one is full!");
    //             //storage = null;
    //         }


    //     }
    //     /*if (hit.gameObject.tag == "Pickable" && Input.GetKeyDown(KeyCode.E) && !isHolding)
    //     {
    //         Debug.Log("Grabbed!");
    //         hit.gameObject.transform.parent = this.transform;
    //         hit.gameObject.transform.localPosition = new Vector3(0, 0, 2);
    //         hit.rigidbody.isKinematic = true;
    //         heldObj = hit.rigidbody;
    //         isHolding = true;
    //     }*/
    // }

    ///
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Pickable")
        {
            displayTipMessage("Pick me up!");
            Debug.Log("You can pick it up!");

            HintText = "'F' to pick up!";
            StartCoroutine(Wait());

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

                if (isHolding)
                {
                    HintText = "'F' to hide";
                    StartCoroutine(Wait());
                }

            }
            else
            {
                storageFull = true;
                Debug.Log("This one is full!");

                if (isHolding)
                {
                    HintText = "Nope, It's full";
                    StartCoroutine(Wait());
                }
                //storage = null;
            }
        }
        else if (hit.gameObject.tag == "Combine" && isHolding)
        {
            combine = hit.gameObject;
            Debug.Log("Combination time!");

            HintText = " 'F' to combine!";
            StartCoroutine(Wait());
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "Pickable")
        {
            GrabAndDrop item = hit.gameObject.GetComponent<GrabAndDrop>();
            if (item != null)
                item.SendMessage("Highlight");
            ///
            //displayTipMessage("Pick me up!");
            //Debug.Log("You can pick it up!");
            potentialHeldObj = hit.GetComponent<Rigidbody>();
        }
        if (hit.gameObject.tag == "Storage")
        {
            StoringItems store = hit.gameObject.GetComponent<StoringItems>();
            if (store != null)
                store.SendMessage("Highlight");
            ///
            storage = hit.gameObject;
            if (store.stored == null)
            {
                storageFull = false;
                //Debug.Log("You can store stuff!");
            }
            else
            {
                storageFull = true;
                //Debug.Log("This one is full!");
                //storage = null;
            }
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        potentialHeldObj = null;
        storage = null;
        combine = null;
        if (hit.gameObject.tag == "Pickable")
        {
            GrabAndDrop item = hit.gameObject.GetComponent<GrabAndDrop>();
            if (item != null)
                item.SendMessage("DeHighlight");
        }
        if (hit.gameObject.tag == "Storage")
        {
            StoringItems store = hit.gameObject.GetComponent<StoringItems>();
            if (store != null)
                store.SendMessage("DeHighlight");
        }
    }
    ///

    //private void OnCollisionExit(Collision hit)
    //{
    //    potentialHeldObj = null;
    //    storage = null;
    //}
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

        HintText = "OH ON! I'm busted! :C";
        StartCoroutine(Wait());

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

    IEnumerator Stun(float time)
    {
        this.enabled = false;

        yield return new WaitForSeconds(time);

        this.enabled = true;
    }
}