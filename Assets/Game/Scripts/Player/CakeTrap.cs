using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeTrap : MonoBehaviour {

    public float time = 5;

    public void Activate(GameObject who)
    {
        // Ktoś kto ma być ogłuszony musi implementować funkcję "stun"
        Debug.Log("Cake!");
        who.SendMessage("Stun", time);
    }
}
