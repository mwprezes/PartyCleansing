using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
        void Start()
    {
        this.GetComponent<GrabAndDrop>().Score_for_Item = 1;
        this.GetComponent<GrabAndDrop>().Weight = 1;
    }
 }
