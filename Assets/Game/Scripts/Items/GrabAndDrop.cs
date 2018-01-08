using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabAndDrop : NetworkBehaviour
{

    private GameObject stuff;
    private Renderer render;
    private Color BasicColor;
    private Color OnMouseColor = Color.green;
    static public bool onObj = false;

    public int Score_for_Item;
    public float Weight;

    //[SyncVar]
    [SyncVar(hook = "OnChangeTag")]
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
        PlayerController.Score = PlayerController.Score + score_for_this_item;
    }

    public void ChgTag(string newtag)
    {
        if (isServer)
        {
            Debug.Log("Yo Server");
            tag = newtag;
        }
    }

    public void OnChangeTag(string newTag)
    {
        tag = newTag;
    }
}
