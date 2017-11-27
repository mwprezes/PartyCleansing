﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyControllerM : NetworkBehaviour
{
    private float waitTime = 0;
    private float maxTime = 5;

    public float speed = 5;
    private Rigidbody rig;

    Rigidbody potentialHeldObj;
    GameObject potTest;

    Rigidbody heldObj;

    private GameObject storage;
    private bool storageFull;
    private GameObject combine;

    Ray cameraRay;
    RaycastHit cameraRayHit;
    RaycastHit hit;

    StoringItems store;
    Lockpicking lockpick;
    void Start()
    {
        rig = GetComponent<Rigidbody>();

    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
            Vector3 rot = new Vector3(hAxis, 0, vAxis);
            if (rot != new Vector3(0, 0, 0))
                transform.rotation = Quaternion.LookRotation(rot);

            rig.MovePosition(transform.position + movement);

            rig.MoveRotation(transform.rotation);

            if ((hit.collider.CompareTag("Storage")) && (Input.GetKeyDown(KeyCode.F)))
            {
                if (store.locked == true)
                    lockpick.SendMessage("Lockpicking_Menu", 4);

                if (store.locked == false)
                {
                    if (store != null && !storageFull) Debug.Log("Pusto");
                    else if (store != null && storageFull) storage.SendMessage("GiveItem", this.name);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(isLocalPlayer)
        {
            if (hit.gameObject.tag == "Storage")
            {
                storage = hit.gameObject;
                store = storage.GetComponent<StoringItems>();
            }
        }
        
    }
}
