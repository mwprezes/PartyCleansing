using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public GameObject other;
    public Vector3 offset;
    private bool fly;

    void Start()
    {
        offset = new Vector3(0, -2, 0);
        fly = false;
        this.GetComponent<Combination>().AllowedWeight = 3;
    }

    private void FixedUpdate()
    {
        if (fly)
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0));
    }

    public void Combine()
    {
        if ((other != null) && (other.GetComponent<GrabAndDrop>().Weight < 3))
        {
            other.transform.parent = this.transform;
            other.transform.localPosition = offset;
            other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<PlayerController>().Carried_Weight = 0;
            MeshRenderer[] tip = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer ren in tip)
            {
                if (ren.gameObject.transform.parent != null && ren.gameObject.tag == "Untagged")
                {
                    ren.enabled = false;
                    Debug.Log("Stuck");
                }
            }
            Fly();
        }
    }

    public void ItemForCombination(GameObject item)
    {
        this.other = item;
        Combine();
    }

    void Fly()
    {
        fly = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0));
        //other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0));
    }
}
