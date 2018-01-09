using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool locked = true;
    private GameObject door;
    private Vector3 here;
    private Vector3 away;

    void Start()
    {
        locked = true;
        door = this.gameObject;
        here = door.transform.position;
        away = here;
        away.y = here.y - 15.0f;
    }

    void Update()
    {
        if (locked) door.transform.position = here;
        else door.transform.position = away;
    }
}
