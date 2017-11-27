using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractItem : MonoBehaviour {

    private GameObject stuff;
    private Renderer render;
    private Color BasicColor;
    private Color OnMouseColor = Color.red;

    public Color Active1 = Color.blue;

    private AudioSource SourceMusic;
    //public AudioClip radio;

    static public bool onObj = false;

    public bool isActive = false;

    EnemyStates enemy;

	// Use this for initialization
	void Start ()
    {
        render = GetComponent<Renderer>();
        BasicColor = render.material.color;
        SourceMusic = GetComponent<AudioSource>();

    }


    void Highlight()
    {
        onObj = true;
        render.material.color = OnMouseColor;
    }

    void DeHighlight()
    {
        onObj = false;
        render.material.color = BasicColor;
    }

    void TurnOn()
    {
        isActive = true;
    }

    void TurnOff()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

            render.material.color = Active1;
            //well... play sth
            // and..
            // change enemystate to distracted;         



        } else { SourceMusic.Play(); }
	}
}
