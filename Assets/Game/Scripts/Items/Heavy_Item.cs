using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Heavy_Item : MonoBehaviour
{
        void Start()
    {
        this.GetComponent<GrabAndDrop>().Score_for_Item = 3;
		this.GetComponent<GrabAndDrop>().SetScore(3);
        this.GetComponent<GrabAndDrop>().Weight = 4;
    }
 }
