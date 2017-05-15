using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[AddComponentMenu("FluxUI/Flux Menu StateManager")]
public class FluxMenuStateManager : MonoBehaviour
{
    public static FluxMenuStateManager Instance;

    Stack<FluxMenu> MenuStack = new Stack<FluxMenu>();

    public FluxMenu DefaultMenu;
    public FluxMenu ExitMenu;

    private FluxMenu CurrentMenu;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        AddStackedMenu(DefaultMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ExitMenu)
            {
                AddStackedMenu(ExitMenu);
            }
        }
    }

    /// <summary>
    /// Adds a Stacked Menu to the Hierarchy 
    /// </summary>
    /// <param name="menu">FluxMenu to Add to the Hierarchy</param>
    public void AddStackedMenu(FluxMenu menu)
    {
        if (CurrentMenu == menu)
            return;

        if (CurrentMenu && !CurrentMenu.IsOpen || menu.GetAnimationState() != FluxMenu.AnimationState.CloseIdle)
            return;

        if (!menu.Popup)
        {
            foreach (FluxMenu prevMenu in MenuStack)
            {
                HideMenu(prevMenu);
            }
        }

        if (CurrentMenu && CurrentMenu.Popup)
        {
            HideMenu(MenuStack.Pop());
            CurrentMenu = null;
        }

        MenuStack.Push(menu);

        ShowMenu(menu);
    }

    /// <summary>
    /// Removes all the Previous Hierarachy and Adds a Stacked Menu
    /// </summary>
    /// <param name="menu">FluxMenu to Add to the Hierarchy</param>
    public void SetCurrentMenu(FluxMenu menu)
    {
        if (CurrentMenu == menu)
            return;

        if (CurrentMenu && !CurrentMenu.IsOpen || menu.GetAnimationState() != FluxMenu.AnimationState.CloseIdle)
            return;

        foreach (FluxMenu prevMenu in MenuStack)
        {
            HideMenu(prevMenu);
        }

        CurrentMenu = null;

        MenuStack.Clear();

        AddStackedMenu(menu);
    }

    /// <summary>
    /// Go to the Previous Menu of the Hierarchy
    /// </summary>
    public void GotoPrevious()
    {
        FluxMenu PrevMenu = MenuStack.Count > 0 ? MenuStack.Pop() : null;
        FluxMenu TopMenu = MenuStack.Count > 0 ? MenuStack.Peek() : null;

        if(TopMenu && PrevMenu)
        {
            if (CurrentMenu && CurrentMenu.IsOpen &&
            (TopMenu.GetAnimationState() == FluxMenu.AnimationState.CloseIdle && !PrevMenu.Popup) ||
            (TopMenu.GetAnimationState() == FluxMenu.AnimationState.OpenIdle && PrevMenu.Popup))
            {
                if (MenuStack.Count > 0)
                {
                    ShowMenu(TopMenu);
                }
                else
                {
                    ShowMenu(null);
                }
            }
            else
            {
                MenuStack.Push(PrevMenu);
            }
        }
        else
        {
            MenuStack.Push(PrevMenu);
        }
    }

    /// <summary>
    /// Hides the Menu
    /// </summary>
    /// <param name="menu">Menu to be hidden</param>
    void HideMenu(FluxMenu menu)
    {
        menu.IsOpen = false;
    }

    /// <summary>
    /// Displays a Menu
    /// </summary>
    /// <param name="Current">Menu to be Displayed</param>
    void ShowMenu(FluxMenu Current)
    {
        bool isPrevPop = false;

        if (CurrentMenu != null && !Current.Popup)
        {
            isPrevPop = CurrentMenu.Popup;
            HideMenu(CurrentMenu);
            CurrentMenu = null;
        }

        if (Current)
        {
            CurrentMenu = Current;
            CurrentMenu.Show();
            if (!isPrevPop)
            {
                // It should be already opened
                CurrentMenu.IsOpen = true;
            }
        }
    }

    ///////////////////////////////////////////////////////

    /// <summary>
    /// Adds a New Menu to the Scene Hierarchy
    /// </summary>
    /// <returns>Returns the GameObject of the New Menu</returns>
    public GameObject AddMenuObject()
    {
        GameObject Obj = new GameObject();
        Obj.name = "Blank Menu";

        Obj.AddComponent<FluxMenu>();
        Obj.GetComponent<FluxMenu>().StateManager = this;

        Obj.transform.SetParent(transform, false);

        return Obj;
    }
}
