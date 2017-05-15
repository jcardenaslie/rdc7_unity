using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsableButtonList : MonoBehaviour 
{
    private List<CollapsableButtonItem> items = new List<CollapsableButtonItem>();

    public void AddItem(CollapsableButtonItem item)
    {
        items.Add(item);
    }

    public void StartOpeningAnimation()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].StartOpeningAnimation();
        }
    }

    public void StartClosingAnimation()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].StartClosingAnimation();
        }
    }
}
