using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public GameObject other;
    public Vector3 offset;
    private GrabAndDrop itm;
    public int weight;

    void Start()
    {
        offset = new Vector3(0, -1, 0);
        this.GetComponent<Combination>().AllowedWeight = 3;
        weight = 10;
    }

    private void FixedUpdate()
    {
    }

    public void Combine()
    {
        if ((other != null) && (other.GetComponent<GrabAndDrop>().Weight < 3))
        {
            this.transform.parent = other.transform;
            this.transform.localPosition = offset;
            this.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<PlayerController>().Carried_Weight = itm.Weight;
            itm.Weight = weight;
        }
    }

    public void ItemForCombination(GameObject item)
    {
        this.other = item;
        itm = other.GetComponent<GrabAndDrop>();
        Combine();
    }
}
