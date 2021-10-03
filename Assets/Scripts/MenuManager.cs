using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager class for view menu
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static MenuManager Instance;



    /// <summary>
    /// List of menu
    /// </summary>
    [SerializeField]
    Menu[] menus;



    /// <summary>
    /// Open the menu
    /// </summary>
    /// <param name="menuName">Name of menu</param>
    public void OpenMenu(string menuName)
    {
        foreach (var menu in menus)
        {
            if (menu.menuName == menuName)
            {
                OpenMenu(menu);
            }
            else if (menu.open)
            {
                CloseMenu(menu);
            }
        }
    }

    /// <summary>
    /// Open the menu
    /// </summary>
    /// <param name="menu">Instance of menu object</param>
    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }

    /// <summary>
    /// Close the menu
    /// </summary>
    /// <param name="menu">Instance of menu object</param>
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    /// <summary>
    /// Close the application
    /// </summary>
    public void AppQuit()
    {
        Application.Quit();
    }



    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake() {
        Instance = this;
    }
}
