using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyControllerM : NetworkBehaviour
{
    private float waitTime = 0;
    private float maxTime = 5;

    public float speed = 5;
    private Rigidbody rig;
    private bool mouseLock = true;
    private ProgressBar pBar;

    Rigidbody potentialHeldObj;
    GameObject potTest;

    Rigidbody heldObj;

    private GameObject storage;
    private bool storageFull;
    private GameObject combine;

    private Vector3 camera_rotate;
    Ray cameraRay;
    RaycastHit cameraRayHit;
    RaycastHit hit;

    StoringItems store;
    Lockpicking lockpick;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rig = GetComponent<Rigidbody>();
        //Instantiate(GameObject.Find("Camera FPS"));
        //var camera = GameObject.Find("Camera FPS(Clone)");
        var camera = GameObject.Find("CameraFPS");

        if (!isLocalPlayer)
        {
            camera.GetComponent<Camera>().enabled = false;
            return;
        }

        //camera_rotate = new Vector3(-40.0f, 0.0f, 0.0f);
        //Destroy(gameObject.GetComponent<EnemyStates>());
        pBar = GetComponent<ProgressBar>();

        rig.isKinematic = false;
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        var follow = camera.GetComponent("FPScamController");
        follow.GetComponent<FPScamController>().targ = this.transform;
        //follow.GetComponent<SmoothCamController>().transform.Rotate(camera_rotate);

    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (mouseLock)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            mouseLock = !mouseLock;
        }

        float hAxis = Input.GetAxis("Horizontal") * speed;
        float vAxis = Input.GetAxis("Vertical") * speed;

        hAxis *= Time.deltaTime;
        vAxis *= Time.deltaTime;

        transform.Translate(hAxis, 0, vAxis);

        //Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
        /*Vector3 rot = new Vector3(hAxis, 0, vAxis);
        if (rot != new Vector3(0, 0, 0))
            transform.rotation = Quaternion.LookRotation(rot);*/

        //rig.MovePosition(transform.position + movement);

        //rig.MoveRotation(transform.rotation);

        if ((storage != null) && (Input.GetKeyDown(KeyCode.F)))
        {
            StartCoroutine(Stun(1.0f));
            //Pasek "searching" czy coś na gui by się przydał

            if (store.locked == true)
                lockpick.SendMessage("Lockpicking_Menu", 4);

            if (store.locked == false)
            {
                if (store != null && !storageFull) Debug.Log("Pusto");
                else if (store != null)
                    if (store.Storage.Count > 0)
                        storage.SendMessage("GiveItem", this.name);
                //else if (store != null && storageFull) storage.SendMessage("GiveItem", this.name);
            }
        }
        if ((potentialHeldObj != null) && (Input.GetKeyDown(KeyCode.F)))
        {
            pickUp(potentialHeldObj.gameObject);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(isLocalPlayer)
        {
            if (hit.gameObject.tag == "Storage")
            {
                storage = hit.gameObject;
                store = storage.GetComponent<StoringItems>();
            }

            if(hit.gameObject.tag == "Pickable")
            {
                if (hit.gameObject.GetComponent<GrabAndDrop>().tag == "Pickable")
                    potentialHeldObj = hit.gameObject.GetComponent<Rigidbody>();
            }
        }        
    }

    private void OnTriggerStay(Collider hit)
    {
        if (isLocalPlayer)
        {
            if (hit.gameObject.tag == "Storage")
            {
                storage = hit.gameObject;
                store = storage.GetComponent<StoringItems>();
            }

            if (hit.gameObject.tag == "Pickable")
            {
                if (hit.gameObject.GetComponent<GrabAndDrop>().tag == "Pickable")
                    potentialHeldObj = hit.gameObject.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        potentialHeldObj = null;
        storage = null;
    }

    IEnumerator Stun(float time)
    {
        if (isLocalPlayer)
        {
            pBar.rate = 100 * pBar.time / time;
            pBar.active = true;
            Debug.Log("rate" + pBar.rate);

            this.enabled = false;

            yield return new WaitForSeconds(time);

            this.enabled = true;
            //pBar.active = false;

        }
    }

    private void pickUp(GameObject body)
    {
        Debug.Log("#Enemy: Woosh!");

        //HintText = "Whoosh!";
        StartCoroutine(Stun(0.5f));
        //Stun(5.0f);

        body.gameObject.transform.parent = this.transform;
        body.gameObject.transform.localPosition = new Vector3(0, 0, 2);
        body.GetComponent<Rigidbody>().isKinematic = true;

        body.gameObject.transform.parent = null;
        body.GetComponent<Rigidbody>().isKinematic = false;
        //if (body.tag != "Busted") body.tag = "Busted";
        //if (isServer)
        {
            //Debug.Log("Yo Server");
            //if (body.GetComponent<GrabAndDrop>().tag != "Busted") body.GetComponent<GrabAndDrop>().tag = "Busted";
            if (body.GetComponent<GrabAndDrop>().tag != "Busted") CmdBustItem(body);
        }
        
    }

    [Command]
    private void CmdBustItem(GameObject body)
    {
        body.GetComponent<GrabAndDrop>().ChgTag("Busted");
    }

    public void ReciveItem(GameObject rec)
    {
        if (rec != null)
        {
            Debug.Log("#Enemy: Znaleziony");
            pickUp(rec);

            //HintText = "Ha! Found YOU!";
            StartCoroutine(Wait());

        }
        else
        {
            Debug.Log("#Enemy: Pusto");

            //HintText = "Empty...";
            StartCoroutine(Wait());
        }
    }
}
