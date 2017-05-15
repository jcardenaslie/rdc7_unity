using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitList : MonoBehaviour 
{
    [System.Serializable]
    public class RDCItem
    {
        public string name;
        public string sceneName;
    }

    [System.Serializable]
    public class Unit
    {
        public string name;
        public RDCItem[] rdcList;
    }

    public Unit[] units;
    public RDC.NavigationMenu.Menu menu;
    public GameObject collapsableButtonPrefab;
    public GameObject collapsableListPrefab;
    public GameObject collapsableItemPrefab;

    private List<CollapsableButton> buttons;

	void Start () 
    {
        buttons = new List<CollapsableButton>();
        int rdcCounter = 1;

        for (int i = 0; i < units.Length; i++)
        {
            GameObject go = Instantiate(collapsableButtonPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            CollapsableButton collapsableButtonScript = go.GetComponent<CollapsableButton>();
            collapsableButtonScript.SetHeader((i + 1).ToString());
            collapsableButtonScript.SetCaption(units[i].name);

            go = Instantiate(collapsableListPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            CollapsableButtonList list = go.GetComponent<CollapsableButtonList>();
            float rdcButtonHeight = 0;

            for (int j = 0; j < units[i].rdcList.Length; j++)
            {
                GameObject rdcButton = Instantiate(collapsableItemPrefab);
                rdcButton.transform.SetParent(go.transform);
                rdcButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                rdcButtonHeight = rdcButton.GetComponent<RectTransform>().sizeDelta.y;
                CollapsableButtonItem currentItem = rdcButton.GetComponent<CollapsableButtonItem>();
                currentItem.SetHeader("RDC " + (rdcCounter++).ToString());
                currentItem.SetCaption(units[i].rdcList[j].name);
                currentItem.Init();
                list.AddItem(currentItem);
            }

            RectTransform listRectTransform = go.transform.GetComponent<RectTransform>();
            listRectTransform.sizeDelta = new Vector2(listRectTransform.sizeDelta.x, rdcButtonHeight * units[i].rdcList.Length);

            collapsableButtonScript.SetCollapsableList(go);

            collapsableButtonScript.onButtonOpened = delegate(CollapsableButton cb)
            {
                    if(menu.Open())
                    {
                        menu.toggleButton.SwitchSprite();
                    }

                    for(int k = 0; k < buttons.Count; k++)
                    {
                        if(cb != buttons[k])
                        {
                            buttons[k].Close();
                        }
                    }
            };

            menu.onClose = delegate
            {
                for (int k = 0; k < buttons.Count; k++)
                {
                    buttons[k].Close();
                }                  
            };

            buttons.Add(collapsableButtonScript);
        }
	}
	
    public void Expand()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Expand();
        }
    }

    public void Collapse()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Collapse();
        }
    }
}
