using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P6Controller : MonoBehaviour {

    public Image[] objetoPintable;
    public ItemColor[] colores;
    private GameObject btnNext;
    private int totalObjetosPintables;
    // Use this for initialization

    void Start()
    {
        totalObjetosPintables = objetoPintable.Length;
        DeactivateNextButton();
    }

    private void DeactivateNextButton()
    {
        Transform panelMenu = this.transform.FindChild("BottomMenu");
        btnNext = panelMenu.transform.FindChild("Next").gameObject;
        btnNext.SetActive(false);
    }

    private void Win() {
        btnNext.SetActive(true);
    }

    public void CheckGameState()
    {
        Debug.Log("checkGame");
        int indexCount = 0;
        for (int i = 0; i < objetoPintable.Length; i++)
        {
            if (objetoPintable[i].GetComponent<Image>().color != Color.black)
            {
                indexCount++;
            }
        }

        if (indexCount == totalObjetosPintables)
        {
            Win();
        }
    }

    public void StartWindow() {
        btnNext.SetActive(false);
        for (int i = 0; i < objetoPintable.Length; i++) {
            objetoPintable[i].color = Color.black;
        }
        for (int j =0; j < colores.Length; j++) {
            colores[j].RestartPosition();
        }
    }
}
