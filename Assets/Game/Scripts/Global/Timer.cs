using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private string textTime; 

    public int playtime = 0;
    public int seconds = 0;
    public int minutes = 0;



    void Start()
    {
        StartCoroutine("Time_In_Game");
    }

    private IEnumerator Time_In_Game()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime++;
            seconds = playtime % 60;
            minutes = playtime / 60;
        }
    }

    void OnGUI()
    {
        GUI.color = Color.magenta;
        GUI.skin.label.fontSize = 21;

        if (seconds >= 10) textTime = minutes + ":" + seconds;
        else textTime = minutes + ":0" + seconds;

        GUI.Label(new Rect(360, 10, 200, 250), textTime);
    }

}
