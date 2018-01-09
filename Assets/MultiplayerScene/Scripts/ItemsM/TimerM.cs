using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimerM : NetworkBehaviour
{
    private string textTime;

    [SyncVar] public int playtime = 0;
    [SyncVar] public int Server_seconds = 0;
    [SyncVar] public int Server_minutes = 0;
    public int seconds = 0;
    public int minutes = 0;


    void Start()
    {
        StartCoroutine("Time_In_Game");
        if (isServer)
        {
            if (isLocalPlayer)
            {
                Server_seconds = 0;
                Server_minutes = 0;
            }
        }
        else if (isLocalPlayer)
        {
            seconds = Server_seconds;
            minutes = Server_minutes;
        }
    }

    private IEnumerator Time_In_Game()
    {
        while (true)
        {
            if (isServer)
            {
                yield return new WaitForSeconds(1);
                playtime++;
                Server_seconds = playtime % 60;
                Server_minutes = playtime / 60;
            }

            seconds = Server_seconds;
            minutes = Server_minutes;
        }
    }

    void OnGUI()
    {
                GUI.color = Color.black;
                GUI.skin.label.fontSize = 22;

                if (seconds >= 10) textTime = minutes + ":" + seconds;
                else textTime = minutes + ":0" + seconds;

                GUI.Label(new Rect(360, 10, 200, 250), textTime);
     }
}
