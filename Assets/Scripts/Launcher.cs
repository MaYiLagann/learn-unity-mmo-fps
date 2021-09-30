using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// <para>Using connecting to photon network server</para>
/// <see href="https://www.youtube.com/playlist?list=PLhsVv9Uw1WzjI8fEBjBQpTyXNZ6Yp1ZLw">See Photon Network Tutorial</see>
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Event when connected to master server
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // base.OnConnectedToMaster();
        Debug.Log("@Launcher - Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // base.OnJoinedLobby();
        Debug.Log("@Launcher - Joined Lobby");
        MenuManager.Instance.OpenMenu("title");
    }



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Debug.Log("@Launcher - Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }
}
