using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private GrabAndDrop item;

    /*void Update ()
    {
        drown();
    }*/

    public void drown()
    {
        //item.gameObject.GetComponent<Collider>().isTrigger = true;
        Collider[] col = item.gameObject.GetComponentsInParent<Collider>();
        foreach (Collider colis in col)
        {
            colis.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log("I am water");
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Pickable" && hit.gameObject.transform.parent == null)
        {
            /*GameObject[] child = hit.GetComponentsInChildren<GameObject>();
            if (child.Length > 1)
            {
                
            }*/
            Debug.Log("i have no parent");

            item = hit.GetComponent<GrabAndDrop>();
            if (item.Weight >= 10)
            {
                Debug.Log("drown");

                drown();
            }
        }
    }
}
