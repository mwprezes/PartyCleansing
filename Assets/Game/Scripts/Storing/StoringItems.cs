using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoringItems : MonoBehaviour
{
    public GameObject stored;
    public Vector3 offset;
    private GameObject whosLooking;
    static public bool onObj = false;

    private Renderer render;
    private Color BasicColor;
    private Color OnMouseColor = Color.green;

    // Use this for initialization
    void Start () {
		offset = new Vector3(0,0,0);
        render = GetComponent<Renderer>();
        BasicColor = render.material.color;
        stored = null;
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
    void Update () {
		
	}

    void Store(GameObject obj)
    {
        if (stored == null)
        {
            stored = obj;
            stored.tag = "Stored";
            stored.transform.parent = this.transform;
            stored.transform.localPosition = offset;
            stored.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void GiveItem(string who)
    {
        if (stored != null)
        {
            stored.tag = "Pickable";
            stored.transform.parent = this.transform;
            stored.transform.localPosition = offset;
            stored.GetComponent<Rigidbody>().isKinematic = true;

            whosLooking = GameObject.Find(who);
            whosLooking.SendMessage("ReciveItem", stored);

            PlayerController.Score = PlayerController.Score - stored.GetComponent<GrabAndDrop>().Score_for_Item;

            stored = null;
        }
        /*else // SendMessage nie może wysyłać null
        {
            whosLooking = GameObject.Find(who);
            whosLooking.SendMessage("ReciveItem", stored);
        }*/
    }
}
