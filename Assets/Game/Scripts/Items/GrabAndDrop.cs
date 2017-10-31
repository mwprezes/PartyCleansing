using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{

    private GameObject stuff;
    private Renderer render;
    private Color BasicColor;
    private Color OnMouseColor = Color.green;
    static public bool onObj = false;

    public int Score_for_Item;
    public int Weight;

    // Use this for initialization
    void Start()
    {
        render = GetComponent<Renderer>();
        BasicColor = render.material.color;
    }

    void OnMouseOver()
    {
        onObj = true;
        render.material.color = OnMouseColor;
    }

    void OnMouseExit()
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
        Score_for_Item = score_for_this_item;
    }
}
