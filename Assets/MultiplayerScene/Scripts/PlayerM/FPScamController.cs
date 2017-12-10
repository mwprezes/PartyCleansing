using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPScamController : MonoBehaviour
{

    public Transform targ;
    private bool hasParent = false;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (hasParent)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90.0f, 90.0f);
            mouseLook += smoothV;

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            targ.localRotation = Quaternion.AngleAxis(mouseLook.x, targ.up);
        }
    }

    private void LateUpdate()
    {
        if (targ && !hasParent)
        {
            Debug.Log("I am an orphan no longer!");
            this.transform.parent = targ;
            this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f);
            this.transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
            hasParent = true;
        }
    }
}
