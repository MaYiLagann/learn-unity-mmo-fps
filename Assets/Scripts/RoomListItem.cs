using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

/// <summary>
/// Class for RoomListItem prefab
/// </summary>
public class RoomListItem : MonoBehaviour
{
    /// <summary>
    /// Text component for room name
    /// </summary>
    [SerializeField] TMP_Text text;

    /// <summary>
    /// Room info by photon network
    /// </summary>
    RoomInfo info;



    /// <summary>
    /// Setup room info
    /// </summary>
    /// <param name="info">Room info by photon network</param>
    public void Setup(RoomInfo info)
    {
        this.info = info;
        text.text = info.Name;
    }

    /// <summary>
    /// Event when on click button
    /// </summary>
    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
