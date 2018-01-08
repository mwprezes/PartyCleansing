using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public float progress = 0;
    public float fullBar = 100;

    private GameObject canvas;
    public Slider progressBar;

    public bool active = false;
    bool done = true;
    public float rate = 0;
    public float time = 0.01f;

    private float CalculateProgress()
    {
        return progress / fullBar;
    }

    private void Start()
    {
        canvas = GameObject.Find("ProgressCanvas");
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
        progressBar.value = CalculateProgress();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (active)
        {
            canvas.SetActive(true);
            //if (done)
            {
                //Debug.Log("Activated canvas");
                StartCoroutine(UpdateProgress());
                progressBar.value = CalculateProgress();
            }
        }
        else
            canvas.SetActive(false);
    }

    IEnumerator UpdateProgress()
    {
        progress += rate;
        //Debug.Log("Progress: " + progress);
        done = false;
        yield return new WaitForSeconds(time);
        //Debug.Log("Hello after time");

        if (progress >= fullBar)
        {
             Debug.Log("Done: " + progress);
            active = false;
            progress = 0;
            rate = 0;
        }
        done = true;
    }
}
