using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public int window;
	public GUISkin skin;
	public float buttonWidth = 540;
	public float buttonHeight = 90;
	public float screenMargin = 50;
	public Texture control;

	private float buttonMargin = 10;

	void Start ()
    {
        window = 1;
		buttonWidth = (buttonWidth * Screen.width) / 1920;
		buttonHeight = (buttonHeight * Screen.height) / 1080;
		buttonMargin = (buttonMargin * Screen.height) / 1080;
		screenMargin = (screenMargin * Screen.height) / 1080;
	}


    void OnGUI()
    {
		GUI.skin = skin;
		GUI.BeginGroup(new Rect(100, screenMargin, buttonWidth+50, (buttonHeight * buttonMargin)*4));
        if (window == 1)
        {
			if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "PLAY"))
            {
                window = 2;
            }
			if (GUI.Button(new Rect(0, buttonHeight + buttonMargin, buttonWidth, buttonHeight), "CONTROL"))
            {
                window = 3;
            }
			if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin)*2, buttonWidth, buttonHeight), "CREDITS"))
            {
                window = 4;
            }
			if (GUI.Button(new Rect(0, (buttonHeight + buttonMargin)*3, buttonWidth, buttonHeight), "EXIT"))
            {
                window = 5;
            }
        }

        if (window == 2)
        {
			GUI.Label(new Rect(10, 10, buttonWidth+50, (buttonHeight * buttonMargin)*2), "CHOOSE LEVEL");
			if (GUI.Button(new Rect(10, screenMargin, buttonWidth, buttonHeight), "MULTIPLAYER"))
            {
                Debug.Log("Multi level loaded");
                SceneManager.LoadScene("MultiplayerScene");
            }
			if (GUI.Button(new Rect(10, buttonHeight + buttonMargin + screenMargin, buttonWidth, buttonHeight), "BACK TO MENU"))
			{
				window = 1;
			}
        }

        if (window == 3)
        {
			GUI.Label(new Rect (10, screenMargin, Screen.width/2, Screen.height/2), control);
			if (GUI.Button(new Rect(10, buttonHeight * 7 + screenMargin, buttonWidth,  buttonHeight), "BACK TO MENU"))
            {
                window = 1;
            }
        }

        if (window == 4)
        {
			GUI.Label(new Rect(10, screenMargin, buttonWidth, buttonHeight*6), 
                "Created by: \n" +
                "Baida Bogdan \n" +
				"Olszewski Piotr \n" +
				"Suliborski Łukasz \n" +
				"Szejner Mateusz \n" +
				"Waligórski Marcin");
			if (GUI.Button(new Rect(10, buttonHeight * 6 + screenMargin, buttonWidth, buttonHeight), "BACK TO MENU"))
            {
                window = 1;
            }
        }

        if (window == 5)
            Application.Quit();

        GUI.EndGroup();

    }

}
