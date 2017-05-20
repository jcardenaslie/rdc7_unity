using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P6Controller : MonoBehaviour {

    public Image[] objectoPintable;
    private GameObject btnNext;
    private int totalObjetosPintables;
    // Use this for initialization

    void Start()
    {
        totalObjetosPintables = objectoPintable.Length;
        DeactivateNextButton();
    }

    private void DeactivateNextButton()
    {
        Transform panelMenu = this.transform.FindChild("BottomMenu");
        btnNext = panelMenu.transform.FindChild("Next").gameObject;
        btnNext.SetActive(false);
    }

    public void CheckGameState() {
        Debug.Log("checkGame");
        int indexCount = 0;
        for (int i = 0; i < objectoPintable.Length ; i++) {
            if (objectoPintable[i].GetComponent<Image>().color != Color.black ) {
                indexCount++;
            }
        }

        if (indexCount == totalObjetosPintables ) {
            Win();
        }
    }

    private void Win() {
        btnNext.SetActive(true);
    }
}
