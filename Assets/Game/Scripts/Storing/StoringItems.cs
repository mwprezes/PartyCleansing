using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class StoringItems : NetworkBehaviour
{
    public struct testSynchList
    {
        public GameObject Item1;
        public GameObject Item2;
    };

    public class SynchList : SyncListStruct<testSynchList>
    {

    }

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
    private Color Full = Color.red;

    public SynchList listOfSync = new SynchList();

    [SyncVar]
    public testSynchList gib;
    //GameObject gib = null;

    // Use this for initialization
    void Start () {
		//offset = new Vector3(0,0,0);
        render = GetComponent<Renderer>();
        BasicColor = render.material.color;
        stored = null;
        render.material.SetColor("_OutlineColor", Color.black);

        List<GameObject> Storage = new List<GameObject>();
    }

    void Highlight()
    {
        onObj = true;
        if (!storagefull)
            render.material.SetColor("_OutlineColor", Color.yellow);
        else
        {
            render.material.SetColor("_OutlineColor", Color.red);
        }
        
    }

    void DeHighlight()
    {
        onObj = false;
        render.material.color = BasicColor;
        
        if (storagefull || temperedWith)
            render.material.SetColor("_OutlineColor", Color.red);
        else
            render.material.SetColor("_OutlineColor", Color.black);
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (storagefull || temperedWith) { }
           // render.material.SetColor("_OutlineColor", Color.red);
            //render.material.color = Color.red;
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
                gib.Item1 = obj;
                listOfSync.Add(gib);
                Storage.Add(obj);
                //Stor.Storage.Add(obj);

                foreach (GameObject ob in Storage)
                {
                    //ob.tag = "Stored";
                    CmdChgTag(ob, "Stored");
                    ob.transform.parent = this.transform;
                    ob.transform.localPosition = offset;
                    ob.GetComponent<Rigidbody>().isKinematic = true;
                    // updating tags
                    //List<NetworkClient> clients = new List<NetworkClient>(NetworkClient.allClients);
                    //foreach(NetworkClient c in clients)
                    //{
                    //    //MessageBase 
                    //    //c.Send(777, )
                    //}

                }


                if (Storage.ElementAt(0).GetComponent<GrabAndDrop>().Weight == 4)
                    storagefull = true;
            }
            else if (Storage.Count.Equals(1) && !storagefull)
            {

                if (obj.GetComponent<GrabAndDrop>().Weight < 2)
                {
                    Storage.Add(obj);
                    gib.Item2 = obj;
                    listOfSync.Add(gib);
                    //Storage.ElementAt(1).tag = "Stored";
                    CmdChgTag(obj, "Stored");
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
        Debug.Log("Giving");
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
                Debug.Log("Storage 2");

                //Storage.ElementAt(1).tag = "Pickable";
                CmdChgTag(Storage.ElementAt(1), "Pickable");
                Storage.ElementAt(1).transform.parent = this.transform;
                Storage.ElementAt(1).transform.localPosition = offset;
                Storage.ElementAt(1).GetComponent<Rigidbody>().isKinematic = true;
                //gib = Storage.ElementAt(1);

                whosLooking = GameObject.Find(who);
                whosLooking.SendMessage("ReciveItem", gib.Item2);

                //whosLooking.SendMessage("ReciveItem", listOfSync.ElementAt(1).Item2);
                //whosLooking.SendMessage("ReciveItem", Storage.ElementAt(1));

                Storage.RemoveAt(1);
               
            }
            else if (Storage.Count.Equals(1))
            {
                Debug.Log("Storage 1");

                //Storage.ElementAt(0).tag = "Pickable";
                CmdChgTag(Storage.ElementAt(0), "Pickable");
                Storage.ElementAt(0).transform.parent = this.transform;
                Storage.ElementAt(0).transform.localPosition = offset;
                Storage.ElementAt(0).GetComponent<Rigidbody>().isKinematic = true;
                //gib = Storage.ElementAt(0);

                whosLooking = GameObject.Find(who);
                whosLooking.SendMessage("ReciveItem", gib.Item1);
                //whosLooking.SendMessage("ReciveItem", listOfSync.ElementAt(0).Item1);
                //whosLooking.SendMessage("ReciveItem", Storage.ElementAt(0));

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

    public void AddToStore(GameObject what)
    {
        if (isServer)
        {
            if (!storagefull)
            {
                Storage.Add(what);

                CmdChgTag(what, "Stored");
                gib.Item1 = what;
                gib.Item1.transform.parent = this.transform;
                gib.Item1.transform.localPosition = offset;
                gib.Item1.GetComponent<Rigidbody>().isKinematic = true;

                storagefull = true;
            }
        }
    }

    [Command]
    void CmdChgTag(GameObject ob, string newtag)
    {
        ob.GetComponent<GrabAndDrop>().ChgTag(newtag);
    }
}
