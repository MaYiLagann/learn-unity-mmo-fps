using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Manager class for room
/// </summary>
public class RoomManager : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static RoomManager Instance;



    /// <summary>
    /// Event when enabled
    /// </summary>
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Event when disabled
    /// </summary>
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Event when scene loaded
    /// </summary>
    /// <param name="scene">Scene</param>
    /// <param name="loadSceneMode">Load scene mode</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
}
