using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

/// <summary>
/// Class for display username into text component
/// </summary>
public class UsernameDisplay : MonoBehaviour
{
    /// <summary>
    /// Photon view instance for get username
    /// </summary>
    [SerializeField] PhotonView photonView;

    /// <summary>
    /// Text component for display username
    /// </summary>
    [SerializeField] TMP_Text text;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        if (photonView.IsMine) gameObject.SetActive(false);

        text.SetText(photonView.Owner.NickName);
    }
}
