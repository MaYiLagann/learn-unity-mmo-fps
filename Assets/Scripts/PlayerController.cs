using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for player object controll
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Sensitivity of mouse movement
    /// </summary>
    [SerializeField] float mouseSensitivity;
    /// <summary>
    /// Speed of sprint
    /// </summary>
    [SerializeField] float sprintSpeed;
    /// <summary>
    /// Speed of walk
    /// </summary>
    [SerializeField] float walkSpeed;
    /// <summary>
    /// Force of jump
    /// </summary>
    [SerializeField] float jumpForce;
    /// <summary>
    /// Time for smooth
    /// </summary>
    [SerializeField] float smoothTime;



    /// <summary>
    /// Vertical Look Rotation
    /// </summary>
    float verticalLookRotation;
    /// <summary>
    /// Grounded
    /// </summary>
    bool grounded;
    /// <summary>
    /// Smooth move velocity
    /// </summary>
    Vector3 smoothMoveVelocity;
    /// <summary>
    /// Move amount
    /// </summary>
    Vector3 moveAmount;

    /// <summary>
    /// Self rigidbody
    /// </summary>
    Rigidbody rigidbody;



    /// <summary>
    /// Instance initialization 
    /// </summary>
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
    }
}
