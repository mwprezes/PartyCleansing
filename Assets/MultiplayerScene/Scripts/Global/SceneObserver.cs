using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneObserver : NetworkBehaviour
{

    GameTimer timer;
    PlayerChooseM network;
    static SceneObserver _instance;

    [SyncVar]
    public bool round2 = false;
    public bool end = false;

    //Add this in scene explorer
    public GameObject door;

    public float P1_Points;
    public float P2_Points;

    // Use this for initialization
    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);
    }
        void Start () {
        timer = GameObject.Find("TimerGO").GetComponent<GameTimer>();
        network = GameObject.Find("NetworkManager").GetComponent<PlayerChooseM>();
        //DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
		
        if(NetworkServer.connections.Count >= 2 && !round2)
        {
            //Debug.Log("I see a player");
            timer.start = true;
        }
        if(timer.timeForHiding <= 0 && door != null)
        {
            Debug.Log("HODOR! But the doors are no longer...");
            Destroy(door);
        }
        if (timer.gameover)
        {
            //SceneManager.LoadScene("MultiplayerScene");
            network.ServerChangeScene("MultiplayerScene");
            //NetworkServer.Reset();

            timer.gameover = false;
            round2 = true;
        }
	}
}
