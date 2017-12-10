using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{

    public Transform[] waypoints;
    public int whereStorageRange;
    public int maxTime;
    public AudioClip detected;
    public AudioClip a_stn;

    private DistractItem dsitem;
    private GameObject distractObject;

    [HideInInspector]
    public int wayAllNumber;
    [HideInInspector]
    public int patientLevel = 0;
    [HideInInspector]
    public FirstWaitState firstWaitState;
    [HideInInspector]
    public LookForState lookForState;
    [HideInInspector]
    public SearchState searchState;
    [HideInInspector]
    public WaitState waitState;
    [HideInInspector]
    public DistractState distractState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]

    private bool HintShow = false;
    private string HintText = "";



    void Awake()
    {
        firstWaitState = new FirstWaitState(this);
        lookForState = new LookForState(this);
        searchState = new SearchState(this);
        waitState = new WaitState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        distractState = new DistractState(this);

    }


    // Use this for initialization
    void Start()
    {
        currentState = firstWaitState;

        HintShow = true;
        HintText = "Knock, Knock!";
        StartCoroutine(Wait());

        
    }

    // Update is called once per frame
    void Update()
    {
        

        currentState.UpdateActions();

 
    }

    void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }

    void OnGUI()
    {
        /*if (HintShow)
        {
            GUI.color = Color.white;
            var HintPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            GUI.Label(new Rect(HintPosition.x - 20, Screen.height - HintPosition.y - 70, 250, 25), "<size=18>" + HintText + "</size>");
        }*/
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        HintText = "";
    }


    private void pickUp(GameObject body)
    {
        Debug.Log("#Enemy: Woosh!");

        HintText = "Whoosh!";
        StartCoroutine(Wait());

        body.gameObject.transform.parent = this.transform;
        body.gameObject.transform.localPosition = new Vector3(0, 0, 2);
        body.GetComponent<Rigidbody>().isKinematic = true;

        body.gameObject.transform.parent = null;
        body.GetComponent<Rigidbody>().isKinematic = false;
        if (body.tag != "Busted") body.tag = "Busted";
    }

    public void ReciveItem(GameObject rec)
    {
        if (rec != null)
        {
            Debug.Log("#Enemy: Znaleziony");
            pickUp(rec);

            HintText = "Ha! Found YOU!";
            StartCoroutine(Wait());

        }
        else
        {
            Debug.Log("#Enemy: Pusto");

            HintText = "Empty...";
            StartCoroutine(Wait());
        }
    }

    IEnumerator Stun(float time)
    {
        AudioSource ausrc = GetComponent<AudioSource>();
        ausrc.PlayOneShot(a_stn, 0.5f);

        this.enabled = false;

        yield return new WaitForSeconds(time);

        this.enabled = true;
    }
}
