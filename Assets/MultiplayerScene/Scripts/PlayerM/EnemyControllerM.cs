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
        var camera = GameObject.Find("Camera FPS");

        if (!isLocalPlayer)
        {
            camera.GetComponent<Camera>().enabled = false;
            return;
        }

        //camera_rotate = new Vector3(-40.0f, 0.0f, 0.0f);
        Destroy(gameObject.GetComponent<EnemyStates>());
        

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

        if ((storage) && (Input.GetKeyDown(KeyCode.F)))
            {
                if (store.locked == true)
                    lockpick.SendMessage("Lockpicking_Menu", 4);

                if (store.locked == false)
                {
                    if (store != null && !storageFull) Debug.Log("Pusto");
                    else if (store != null && storageFull) storage.SendMessage("GiveItem", this.name);
                }
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
        }
        
    }
}
