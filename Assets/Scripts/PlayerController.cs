using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Class for player object controll
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviourPunCallbacks, IDamagable
{
    /// <summary>
    /// Event handler for take damage
    /// </summary>
    /// <typeparam name="float">Changed current health</typeparam>
    public UnityAction<float> onTakeDamage;



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
    /// Canvas for drawing UI elements
    /// </summary>
    [SerializeField] Canvas canvas;
    /// <summary>
    /// Health bar gauge image
    /// </summary>
    [SerializeField] Image healthGaugeImage;



    /// <summary>
    /// Index of current item
    /// </summary>
    int itemIndex = -1;
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
    new PhotonView photonView;
    /// <summary>
    /// Player manager
    /// </summary>
    PlayerManager playerManager;

    /// <summary>
    /// Point of max health
    /// </summary>
    const float maxHealth = 100f;
    /// <summary>
    /// Point of current health
    /// </summary>
    float currentHealth = maxHealth;



    /// <summary>
    /// Set grounded state
    /// </summary>
    /// <param name="grounded">Is grounded?</param>
    public void SetGroundedState(bool grounded)
    {
        this.grounded = grounded;
    }

    /// <summary>
    /// Take the damage
    /// </summary>
    /// <param name="damage">Point of damage</param>
    public void TakeDamage(float damage)
    {
        photonView.RPC("RpcTakeDamage", RpcTarget.All, damage);
    }

    /// <summary>
    /// Event when updated player properties
    /// </summary>
    /// <param name="targetPlayer">Target player</param>
    /// <param name="changedProps">Changed properties</param>
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (!photonView.IsMine && targetPlayer == photonView.Owner)
        {
            EquipItem((int)changedProps["ItemIndex"]);
        }
    }



    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();

        // Subscribe event 'TakeDamage'
        onTakeDamage += (health) =>
        {
            if (healthGaugeImage)
            {
                var scale = healthGaugeImage.transform.localScale;
                scale.x = health / maxHealth;
                healthGaugeImage.transform.localScale = scale;
            }
        };
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time
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
            if (canvas) Destroy(canvas.gameObject);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        if (photonView.IsMine)
        {
            Look();
            Move();
            Jump();
            ChangeItem();

            if (Input.GetMouseButtonDown(0))
            {
                items[itemIndex].Use();
            }

            // Die if you fall out of the world
            if (transform.position.y < -10f)
            {
                playerManager.Die();
            }
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    /// </summary>
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// RPC, Take the damage
    /// </summary>
    /// <param name="damage"></param>
    [PunRPC]
    void RpcTakeDamage(float damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;

            onTakeDamage.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                playerManager.Die();
            }
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
    /// Change players item by input number key or mouse scroll
    /// </summary>
    private void ChangeItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (itemIndex < items.Length - 1)
                EquipItem(itemIndex + 1);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (itemIndex > 0)
                EquipItem(itemIndex - 1);
        }
    }

    /// <summary>
    /// Equip the item
    /// </summary>
    /// <param name="index">Index of item list</param>
    private void EquipItem(int index)
    {
        if (index == itemIndex) return;

        itemIndex = index;

        items[itemIndex].itemGameObject.SetActive(true);
        if (prevItemIndex > -1)
        {
            items[prevItemIndex].itemGameObject.SetActive(false);
        }
        prevItemIndex = itemIndex;

        if (photonView.IsMine)
        {
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("ItemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
    }
}
