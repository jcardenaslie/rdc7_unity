using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("FluxUI/Flux Menu")]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(FluxAnimation))]
public class FluxMenu : MonoBehaviour
{
    public enum AnimationState
    {
        Opening,
        OpenIdle,
        Closing,
        CloseIdle
    }

    public FluxMenuStateManager StateManager;
    private FluxAnimation Animation;
    public bool Popup = false;

	private CanvasGroup canvasGroup;

    private AnimationState AnimState = AnimationState.CloseIdle;

    /// <summary>
    /// Returns if the Menu is Open or Closed
    /// </summary>
    public virtual bool IsOpen
	{
        get 
        {
            return AnimState == AnimationState.OpenIdle;
        }
        set 
        {
            if (value)
            {
                if (AnimState == AnimationState.CloseIdle)
                {
                    StartOpenAnimation();
                }
            }
            else
            {
                if (AnimState == AnimationState.OpenIdle)
                {
                    StartCloseAnimation();
                }
            }
        }
	}

    /// <summary>
    /// Returns the Current Animation State
    /// </summary>
    /// <returns></returns>
    public AnimationState GetAnimationState()
    {
        return AnimState;
    }

    /// <summary>
    /// Start Open Animation
    /// </summary>
    private void StartOpenAnimation()
    {
        AddListeners();
        AnimState = AnimationState.Opening;
        Animation.StartOpeningAnimation();
    }

    /// <summary>
    /// Start Close Animation
    /// </summary>
    private void StartCloseAnimation()
    {
        AddListeners();
        AnimState = AnimationState.Closing;
        Animation.StartClosingAnimation();
    }

    void OnAnimStart()
    {
        IsOpen = true;
    }

    void OnAnimEnd()
    {
        if (AnimState == AnimationState.Opening)
        {
            AnimState = AnimationState.OpenIdle;
        }
        else if(AnimState == AnimationState.Closing)
        {
            Hide();
            AnimState = AnimationState.CloseIdle;
        }
    }

	// Use this for initialization
    public virtual void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
        Animation = GetComponent<FluxAnimation>();

		Hide ();
	}

    void Start()
    {
        AddListeners();
    }

    /// <summary>
    /// Adds the listeners
    /// </summary>
    void AddListeners()
    {
        Animation.onOpenStart.RemoveListener(OnAnimStart);
        Animation.onOpenEnd.RemoveListener(OnAnimEnd);

        Animation.onCloseStart.RemoveListener(OnAnimStart);
        Animation.onCloseEnd.RemoveListener(OnAnimEnd);

        Animation.onOpenStart.AddListener(OnAnimStart);
        Animation.onOpenEnd.AddListener(OnAnimEnd);

        Animation.onCloseStart.AddListener(OnAnimStart);
        Animation.onCloseEnd.AddListener(OnAnimEnd);
    }

    /// <summary>
    /// Show the Menu
    /// </summary>
    public virtual void Show()
	{
		gameObject.SetActive(true);
	}

    /// <summary>
    /// Hide the Menu
    /// </summary>
    public virtual void Hide()
	{
		gameObject.SetActive(false);
	}

    /// <summary>
    /// Calls once per Frame, Don't Call Explicitly
    /// </summary>
    public virtual void Update()
	{
        if (canvasGroup)
        {
            if (!IsOpen)
            {
                canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
            }
            else
            {
                canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
            }
        }
     }

    /// <summary>
    /// Returns the Menu State Manager attached to this menu
    /// </summary>
    /// <returns>FluxMenuStateManager</returns>
    public FluxMenuStateManager GetMenuManager()
    {
        return StateManager;
    }

    /// <summary>
    /// Make this the Default Menu
    /// </summary>
    public void MakeDefault()
    {
        StateManager.DefaultMenu = this;
    }

    /// <summary>
    /// Make this the Exit Menu
    /// </summary>
    public void MakeExit()
    {
        StateManager.ExitMenu = this;
    }
}

