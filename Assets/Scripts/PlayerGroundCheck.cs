using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for player grounded check
/// </summary>
public class PlayerGroundCheck : MonoBehaviour
{
    /// <summary>
    /// Player Controller
    /// </summary>
    PlayerController playerController;



    /// <summary>
    /// Instance initialization 
    /// </summary>
    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    /// <summary>
    /// Event when enter trigger
    /// </summary>
    /// <param name="other">Other collider class</param>
    void OnTriggerEnter(Collider other)
    {
        SetGroundedState(true, other.gameObject);
    }

    /// <summary>
    /// Event when exit trigger
    /// </summary>
    /// <param name="other">Other collider class</param>
    void OnTriggerExit(Collider other)
    {
        SetGroundedState(false, other.gameObject);
    }

    /// <summary>
    /// Event when stay trigger
    /// </summary>
    /// <param name="other">Other collider class</param>
    void OnTriggerStay(Collider other)
    {
        SetGroundedState(true, other.gameObject);
    }

    /// <summary>
    /// Event when enter collision
    /// </summary>
    /// <param name="other">Other collision class</param>
    void OnCollisionEnter(Collision other)
    {
        SetGroundedState(true, other.gameObject);
    }

    /// <summary>
    /// Event when exit collision
    /// </summary>
    /// <param name="other">Other collision class</param>
    void OnCollisionExit(Collision other)
    {
        SetGroundedState(false, other.gameObject);
    }

    /// <summary>
    /// Event when stay collision
    /// </summary>
    /// <param name="other">Other collision class</param>
    void OnCollisionStay(Collision other)
    {
        SetGroundedState(true, other.gameObject);
    }



    /// <summary>
    /// Set grounded state in player controller
    /// </summary>
    /// <param name="grounded">Is grounded?</param>
    /// <param name="otherGameObject">Conflicted other gameobject</param>
    private void SetGroundedState(bool grounded, GameObject otherGameObject)
    {
        if (otherGameObject != playerController.gameObject)
            playerController.SetGroundedState(grounded);
    }
}
