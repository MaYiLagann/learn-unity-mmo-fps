using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

/// <summary>
/// <para>Using connecting to photon network server</para>
/// <see href="https://www.youtube.com/playlist?list=PLhsVv9Uw1WzjI8fEBjBQpTyXNZ6Yp1ZLw">See Photon Network Tutorial</see>
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static Launcher Instance;



    /// <summary>
    /// Input component for enter room name
    /// </summary>
    [SerializeField] TMP_InputField roomNameInputField;
    /// <summary>
    /// Text component for error
    /// </summary>
    [SerializeField] TMP_Text errorText;
    /// <summary>
    /// Text component for room name
    /// </summary>
    [SerializeField] TMP_Text roomNameText;
    /// <summary>
    /// Transform for room list content
    /// </summary>
    [SerializeField] Transform roomListContent;
    /// <summary>
    /// Object for room list item prefab
    /// </summary>
    [SerializeField] GameObject roomListItemPrefab;



    /// <summary>
    /// Event when connected to master server
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // base.OnConnectedToMaster();
        Debug.Log("@Launcher - Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Event when joined lobby
    /// </summary>
    public override void OnJoinedLobby()
    {
        // base.OnJoinedLobby();
        Debug.Log("@Launcher - Joined Lobby");
        MenuManager.Instance.OpenMenu("title");
    }

    /// <summary>
    /// Event when joined room
    /// </summary>
    public override void OnJoinedRoom()
    {
        // base.OnJoinedRoom();
        Debug.Log("@Launcher - Joined Room");
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    /// <summary>
    /// Event when failed create room
    /// </summary>
    /// <param name="returnCode">Return code</param>
    /// <param name="message">Message</param>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("@Launcher - Create Room Failed");
        errorText.text = "Room Creation Failed:\n" + message;
        MenuManager.Instance.OpenMenu("error");
    }

    /// <summary>
    /// Event when left room
    /// </summary>
    public override void OnLeftRoom()
    {
        // base.OnLeftRoom();
        Debug.Log("@Launcher - Left Room");
        MenuManager.Instance.OpenMenu("title");
    }

    /// <summary>
    /// Event when update room list
    /// </summary>
    /// <param name="roomList">Room List</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // base.OnRoomListUpdate(roomList);
        Debug.Log("@Launcher - Room List Update");
        foreach (var transform in roomListContent)
        {
            Destroy((transform as Transform).gameObject);
        }
        foreach (var item in roomList)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(item);
        }
    }

    /// <summary>
    /// Create room
    /// </summary>
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text)) return;
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    /// <summary>
    /// Leave room
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    /// <summary>
    /// Join room
    /// </summary>
    /// <param name="info">Room info by photon network</param>
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }



    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake() {
        Instance = this;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Debug.Log("@Launcher - Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

}
