using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDropM : MonoBehaviour
{

    private GameObject stuff;
    private Renderer render;
    private Color BasicColor;
    private Color OnMouseColor = Color.green;
    static public bool onObj = false;

    public int Score_for_Item;
    public float Weight;


    public string tag = "Pickable";


    // Use this for initialization
    void Start()
    {
        render = GetComponent<Renderer>();
        BasicColor = render.material.color;
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
    // Update is called once per frame
    void Update()
    {

    }

    public void SetScore(int score_for_this_item)
    {
        PlayerControllerM.Score = PlayerControllerM.Score + score_for_this_item;
    }
}
