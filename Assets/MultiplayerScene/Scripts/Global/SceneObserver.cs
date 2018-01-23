using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneObserver : NetworkBehaviour
{
    //[SyncVar]
    GameTimer timer;
    public PlayerChooseM network;
    public PlayerControllerM player1;
    public EnemyControllerM player2;
    static SceneObserver _instance;

    [SyncVar]
    public bool round2 = false;

    [SyncVar]
    public bool end = false;
    public int hostID = -1;

    //Add this in scene explorer
    public GameObject door;

    [SyncVar]
    public float P1_Points = 0;
    [SyncVar]
    public float P2_Points = 0;

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

    void Start ()
    {
        timer = GameObject.Find("TimerGO").GetComponent<GameTimer>();
        network = GameObject.Find("NetworkManager").GetComponent<PlayerChooseM>();
        player1 = GameObject.Find("Player(Clone)").GetComponent<PlayerControllerM>();
        player2 = GameObject.Find("Enemy(Clone)").GetComponent<EnemyControllerM>();
        door = GameObject.Find("Door");
        hostID = network.host_id;
        //DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {

        if (timer != null)
        {
            P1_Points = player1.PlayerScore;
            P2_Points = player2.EnemyScore;
            if (network.host_id == -1 && hostID != -1)
            {
                network.host_id = hostID;
            }

            if (network.P1Ready && network.P2Ready && round2)
                timer.start = true;

            if (NetworkServer.connections.Count >= 2 && !round2)
            {
                //Debug.Log("I see a player");
                timer.start = true;
            }
            if (timer.timeForHiding <= 0 && door != null)
            {
                Debug.Log("HODOR! But the doors are no longer...");
                Destroy(door);
            }
            if (timer.gameover)
            {
                //SceneManager.LoadScene("MultiplayerScene");
                if (!end)
                {
                    if (round2)
                    {
                        network.ServerChangeScene("ScoreBoard");
                        round2 = false;
                        end = true;
                    }
                    else
                    {
                        network.ServerChangeScene("MultiplayerScene");
                        //NetworkServer.Reset();

                        timer.gameover = false;
                        round2 = true;
                    }
                }
            }
        }
	}
    private void LateUpdate()
    {
        if (timer != null && door != null)
            return;

        if(timer == null)
            timer = GameObject.Find("TimerGO").GetComponent<GameTimer>();
        if(door == null)
            door = GameObject.Find("Door");

    }
}
