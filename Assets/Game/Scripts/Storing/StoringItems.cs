using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoringItems : MonoBehaviour
{

    public List<GameObject> Storage;
    public bool storagefull = false;
    public bool temperedWith = false;
    public bool locked = false;
    public bool boobytraped = false;
    public string trapName = null;

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

        List<GameObject> Storage = new List<GameObject>();
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

    public void Temper(string type)
    {
        switch (type)
        {
            case "Lock":
                locked = true; break;
            case "Trap":
                boobytraped = true; break;
        }
    }

    void Store(GameObject obj)
    {
        if (!locked)
        {
            if (Storage.Count.Equals(0) && !storagefull)
            {
                Storage.Add(obj);

                foreach (GameObject ob in Storage)
                {
                    ob.tag = "Stored";
                    ob.transform.parent = this.transform;
                    ob.transform.localPosition = offset;
                    ob.GetComponent<Rigidbody>().isKinematic = true;
                }


                if (Storage.ElementAt(0).GetComponent<GrabAndDrop>().Weight == 4)
                    storagefull = true;
            }
            else if (Storage.Count.Equals(1) && !storagefull)
            {

                if (obj.GetComponent<GrabAndDrop>().Weight < 2)
                {
                    Storage.Add(obj);
                    Storage.ElementAt(1).tag = "Stored";
                    Storage.ElementAt(1).transform.parent = this.transform;
                    Storage.ElementAt(1).transform.localPosition = offset;
                    Storage.ElementAt(1).GetComponent<Rigidbody>().isKinematic = true;
                }
                storagefull = true;
            }
        } else
        {
            Debug.Log("It is locked!");
        }

        /*
        if (stored == null)
        {
            stored = obj;
            stored.tag = "Stored";
            stored.transform.parent = this.transform;
            stored.transform.localPosition = offset;
            stored.GetComponent<Rigidbody>().isKinematic = true;
        }*/
    }

    void GiveItem(string who)
    {
        /*
        if(Storage.Count.Equals(2))
        {
            Storage.ElementAt(1).tag = "Pickable";
            Storage.ElementAt(1).transform.parent = this.transform;
            Storage.ElementAt(1).transform.localPosition = offset;
            Storage.ElementAt(1).GetComponent<Rigidbody>().isKinematic = true;

            whosLooking = GameObject.Find(who);
            whosLooking.SendMessage("ReciveItem", Storage.ElementAt(1));

            Storage.RemoveAt(1);
        }*/
        if (locked || boobytraped)
        {
            whosLooking = GameObject.Find(who);
            var trap = this.GetComponent(trapName);
            trap.SendMessage("Activate", whosLooking);
            boobytraped = false;
            //temperedWith = false;
            //return;
        }
        if (Storage.Any() && !locked)
        {

            if (Storage.Count.Equals(2))
            {
                Storage.ElementAt(1).tag = "Pickable";
                Storage.ElementAt(1).transform.parent = this.transform;
                Storage.ElementAt(1).transform.localPosition = offset;
                Storage.ElementAt(1).GetComponent<Rigidbody>().isKinematic = true;

                whosLooking = GameObject.Find(who);
                whosLooking.SendMessage("ReciveItem", Storage.ElementAt(1));

                Storage.RemoveAt(1);
               
            }
            else if (Storage.Count.Equals(1))
            {
                Storage.ElementAt(0).tag = "Pickable";
                Storage.ElementAt(0).transform.parent = this.transform;
                Storage.ElementAt(0).transform.localPosition = offset;
                Storage.ElementAt(0).GetComponent<Rigidbody>().isKinematic = true;

                whosLooking = GameObject.Find(who);
                whosLooking.SendMessage("ReciveItem", Storage.ElementAt(0));

                Storage.RemoveAt(0);
                storagefull = false;          
            }

        } else if (locked)
        {
            Debug.Log("It is locked!");
            //Lockpicking?...
        }

        


        /*
        if (stored != null)
        {
            stored.tag = "Pickable";
            stored.transform.parent = this.transform;
            stored.transform.localPosition = offset;
            stored.GetComponent<Rigidbody>().isKinematic = true;

            whosLooking = GameObject.Find(who);
            whosLooking.SendMessage("ReciveItem", stored);

            stored = null;
        }
        /*else // SendMessage nie może wysyłać null
        {
            whosLooking = GameObject.Find(who);
            whosLooking.SendMessage("ReciveItem", stored);
        }*/
    }
}
