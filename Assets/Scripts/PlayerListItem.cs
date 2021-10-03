using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/// <summary>
/// Class for PlayerListItem prefab
/// </summary>
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// /// Text component for player name
    /// </summary>
    [SerializeField] TMP_Text text;

    /// <summary>
    /// Player info by photon network
    /// </summary>
    Player player;



    /// <summary>
    /// Event when player left room
    /// </summary>
    /// <param name="otherPlayer">Other player</param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // base.OnPlayerLeftRoom(otherPlayer);
        if (otherPlayer == player)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Event when left room
    /// </summary>
    public override void OnLeftRoom()
    {
        // base.OnLeftRoom();
        Destroy(gameObject);
    }

    /// <summary>
    /// Setup player info
    /// </summary>
    /// <param name="player">player info by photon network</param>
    public void Setup(Player player)
    {
        this.player = player;
        text.text = player.NickName;
    }
}
