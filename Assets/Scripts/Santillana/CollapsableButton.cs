using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CollapsableButton : MonoBehaviour, IPointerClickHandler
{
    private RDC.Animations.AlterSize[] alterSizeAnimation;
    private Color closedButtonColor;
    private Image m_Image;
    private GameObject collapsableList;

    public delegate void OnButtonOpened(CollapsableButton cb);
    public TMPro.TMP_Text header;
    public TMPro.TMP_Text caption;
    public Color openedButtonColor;
    public OnButtonOpened onButtonOpened;

    private bool isOpen = false;

	// Use this for initialization
	void Start () 
    {
        alterSizeAnimation = GetComponents<RDC.Animations.AlterSize>();
        m_Image = GetComponent<Image>();
        closedButtonColor = m_Image.color;
	}

    public void Expand()
    {
        alterSizeAnimation[0].Animate();
    }

    public void Collapse()
    {
        alterSizeAnimation[1].Animate();
    }

    public void SetCaption(string caption)
    {
        this.caption.text = caption;
    }

    public void SetHeader(string header)
    {
        this.header.text = header;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        m_Image.color = openedButtonColor;  

        StartOpeningAnimation();

        if (onButtonOpened != null)
        {
            onButtonOpened(this);
        }

        if (collapsableList != null)
        {
            collapsableList.SetActive(true);
        }

        isOpen = true;
    }

    public void Close()
    {
        m_Image.color = closedButtonColor;

        StartClosingAnimation();

        if (collapsableList != null)
        {
            collapsableList.SetActive(false);
        }

        isOpen = false;
    }

    public void SetCollapsableList(GameObject go)
    {
        collapsableList = go;
        go.SetActive(false);
    }

    public void StartOpeningAnimation()
    {
        collapsableList.GetComponent<CollapsableButtonList>().StartOpeningAnimation();
    }

    public void StartClosingAnimation()
    {
        collapsableList.GetComponent<CollapsableButtonList>().StartClosingAnimation();
    }
}
