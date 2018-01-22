using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class PlayerChooseM : NetworkManager
{

    public int chosenCharacter = 0;
    public GameObject[] characters;

    //[SyncVar]
    public int host_id = -1;
    public bool P1Ready = false;
    public bool P2Ready = false;
    //static PlayerChooseM _instance;

    //private void Awake()
    //{
    //    //if we don't have an [_instance] set yet
    //    if (!_instance)
    //        _instance = this;
    //    //otherwise, if we do, kill this thing
    //    else
    //        Destroy(this.gameObject);
    //    DontDestroyOnLoad(this);
    //}

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

        GameObject player;
        Transform startPos = GetStartPosition();

        if (conn.hostId != playerControllerId)
        {
            Vector3 pos = new Vector3(-7.2f, 6.5f, -30.7f);
            player = Instantiate(characters[0], pos, startPos.rotation) as GameObject;
        }
        else
        {
            Vector3 pos = new Vector3(15.5f, 5.6f, -23.2f);
            host_id = playerControllerId;
            Debug.Log("HostID: " + playerControllerId);
            player = Instantiate(characters[1], pos, Quaternion.identity) as GameObject;

        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    public override void OnServerReady(NetworkConnection conn)
    {
        //base.OnServerReady(conn);
        GameObject player;
        Transform startPos = GetStartPosition();
        short playerControllerId;

        if (conn.hostId != host_id)
        {
            Vector3 pos = new Vector3(-7.2f, 6.5f, -30.7f);
            //host_id = conn.hostId;
            Debug.Log("conn.hostId: " + conn.hostId);
            playerControllerId = (short)host_id;
            P1Ready = true;
            player = Instantiate(characters[0], pos, startPos.rotation) as GameObject;
        }
        else
        {
            playerControllerId = 1;
            P2Ready = true;
            Vector3 pos = new Vector3(15.5f, 5.6f, -23.2f);
            player = Instantiate(characters[1], pos, Quaternion.identity) as GameObject;
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
}

