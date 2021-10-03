using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

/// <summary>
/// Manager class for player controller
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Photon view
    /// </summary>
    PhotonView photonView;



    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();
        }
    }

    /// <summary>
    /// Create controller
    /// </summary>
    void CreateController()
    {
        Debug.Log("@PlayerManager - Create Controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
