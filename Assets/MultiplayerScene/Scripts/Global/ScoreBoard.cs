using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreBoard : NetworkBehaviour
{

    SceneObserver ob;
    [SyncVar]
    bool done = false;

	// Use this for initialization
	void Start () {
		
	}

    void Init()
    {
        if (ob != null)
            return;

        ob = GameObject.Find("GameOserver").GetComponent<SceneObserver>();
    }

    // Update is called once per frame
    void Update () {

        Init();

        if(ob != null)
        {
            if (!done)
            {
                GameObject.Find("P1Score").GetComponent<Text>().text = ob.P1_Points.ToString();
                GameObject.Find("P2Score").GetComponent<Text>().text = ob.P2_Points.ToString();
                done = true;
            }
        }
	}
}
