using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamController : MonoBehaviour {

    public Transform targ;
    public float camSpeed = 15;
    public float zOffset = 45;
    public bool smoothFollow = true;
	
	// Update is called once per frame
	void Update () {
        if (targ)
        {
            Vector3 newPos = transform.position;
            newPos.x = targ.position.x;
            newPos.z = targ.position.z - zOffset;

            if (!smoothFollow) transform.position = newPos;
            else transform.position = Vector3.Lerp(transform.position, newPos, camSpeed * Time.deltaTime);
        }
	}
}
