using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI menu instance
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Name of menu
    /// </summary>
    public string menuName;
    /// <summary>
    /// State of menu open
    /// </summary>
    [HideInInspector]
    public bool open;



    /// <summary>
    /// Open menu
    /// </summary>
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Close menu
    /// </summary>
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        open = gameObject.activeSelf;
    }
}
