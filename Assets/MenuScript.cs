using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public int window;

	void Start ()
    {
        window = 1;
	}


    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        if (window == 1)
        {
            if (GUI.Button(new Rect(10, 30, 180, 30), "Play"))
            {
                window = 2;
            }
            if (GUI.Button(new Rect(10, 70, 180, 30), "Settings"))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(10, 110, 180, 30), "Credits"))
            {
                window = 4;
            }
            if (GUI.Button(new Rect(10, 150, 180, 30), "Exit"))
            {
                window = 5;
            }
        }

        if (window == 2)
        {
            GUI.Label(new Rect(60, 10, 180, 30), "Choose level");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Test level"))
            {
                Debug.Log("Test level loaded");
                SceneManager.LoadScene(1);
            }
        }

        if (window == 3)
        {
            if (GUI.Button(new Rect(10, 160, 180, 30), "Back to menu"))
            {
                window = 1;
            }
        }

        if (window == 4)
        {
            GUI.Label(new Rect(10, 40, 180, 200), 
                "Created by: \n" +
                "Baida Bogdan \n" +
                "Lukasz Suliborski \n" +
                "Marcin Waligorski \n" +
                "Piotr Olszewski \n" +
                "Szejku Mateusz");
            if (GUI.Button(new Rect(10, 150, 180, 30), "Back to menu"))
            {
                window = 1;
            }
        }

        if (window == 5)
            Application.Quit();

        GUI.EndGroup();

    }

}
