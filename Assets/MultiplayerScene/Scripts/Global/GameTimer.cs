using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GameTimer : NetworkBehaviour
{

    [SyncVar]
    public bool start;
    [SyncVar]
    public float timeForHiding = 30.0f;
    [SyncVar]
    public float timeForSearch = 45.0f;
    [SyncVar]
    public bool gameover;

    // Use this for initialization
    void Start() {
        start = false;
        gameover = false;


    }

    // Update is called once per frame
    void FixedUpdate() {
        if (start)
        {
            hide();
        }
    }

    void hide()
    {
        timeForHiding -= Time.deltaTime;


        //Debug.Log("TimeLeft:" + timeForHiding);
        if (timeForHiding <= 0)
        {
            search();
        }
    }

    void search()
    {
        timeForSearch -= Time.deltaTime;
        if (timeForSearch <= 0)
        {
            gameover = true;
            start = false;
            timeForSearch = 45.0f;
            timeForHiding = 30.0f;
        }
    }

    void OnGUI()
    {
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 16;

        string textTime = "";

        if (timeForHiding >= 0)
        {
            int inttime = (int)timeForHiding;
            textTime = "Time for Hiding: " + inttime.ToString();
        }
        else if(timeForSearch >= 0)
        {
            int inttime = (int)timeForSearch;
            textTime = "Time for Searching: " + inttime.ToString();
        }

        GUI.Label(new Rect(360, 10, 200, 250), textTime);
    }
}
