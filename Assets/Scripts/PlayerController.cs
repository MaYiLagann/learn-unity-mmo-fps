using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Class for player object controll
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Gameobject for camera holder
    /// </summary>
    [SerializeField] GameObject cameraHolder;
    /// <summary>
    /// Sensitivity of mouse movement
    /// </summary>
    [SerializeField] float mouseSensitivity = 1f;
    /// <summary>
    /// Speed of sprint
    /// </summary>
    [SerializeField] float sprintSpeed = 1f;
    /// <summary>
    /// Speed of walk
    /// </summary>
    [SerializeField] float walkSpeed = 1f;
    /// <summary>
    /// Force of jump
    /// </summary>
    [SerializeField] float jumpForce = 1f;
    /// <summary>
    /// Time for smooth
    /// </summary>
    [SerializeField] float smoothTime = 1f;
    /// <summary>
    /// Item list
    /// </summary>
    [SerializeField] Item[] items;



    /// <summary>
    /// Index of current item
    /// </summary>
    int itemIndex;
    /// <summary>
    /// Index of previous item
    /// </summary>
    int prevItemIndex = -1;
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
    new Rigidbody rigidbody;
    /// <summary>
    /// Self photon view
    /// </summary>
    PhotonView photonView;


    /// <summary>
    /// Set grounded state
    /// </summary>
    /// <param name="grounded">Is grounded?</param>
    public void SetGroundedState(bool grounded)
    {
        this.grounded = grounded;
    }



    /// <summary> 
    /// Instance initialization 
    /// </summary>
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (photonView.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rigidbody);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (photonView.IsMine)
        {
            Look();
            Move();
            Jump();
        }
    }

    /// <summary>
    /// Update per frame for physics calculations
    /// </summary>
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }
    }



    /// <summary>
    /// Move player transform by key input
    /// </summary>
    private void Move()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDirection * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    /// <summary>
    /// Jump player by key input
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }
    }

    /// <summary>
    /// Rotate player camera by mouse move
    /// </summary>
    private void Look()
    {
        // Rotate character axis Y
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        // Rotate character axis X
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    /// <summary>
    /// Equip the item
    /// </summary>
    /// <param name="index">Index of item list</param>
    private void EquipItem(int index)
    {
        itemIndex = index;

        items[itemIndex].itemGameObject.SetActive(true);
        if (prevItemIndex > -1)
        {
            items[prevItemIndex].itemGameObject.SetActive(false);
        }
        prevItemIndex = itemIndex;
    }
}
