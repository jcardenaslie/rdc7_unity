using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3Controller : MonoBehaviour {

    private string currentChoice;
    private string answer = "C";
    private GameObject btnNext;
    private int currentExcercise;
    private int totalExcercise;

    // Use this for initialization
    void Start () {
        DeactivateNextButton();
	}
	

    private void DeactivateNextButton()
    {
        Transform panelMenu = this.transform.FindChild("BottomMenu");
        btnNext = panelMenu.transform.FindChild("Next").gameObject;
        btnNext.SetActive(false);
    }

    private void CheckGameState() {
        if (currentChoice == answer && currentExcercise < totalExcercise)
        {
            btnNext.SetActive(true);
            Debug.Log("Right Choice");
        }
        else {
            Debug.Log(currentChoice);
        }
        
    }

    public void OpcionA() {
        currentChoice = "A";
        CheckGameState();
    }

    public void OpcionB()
    {
        currentChoice = "B";
        CheckGameState();
    }

    public void OpcionC()
    {
        currentChoice = "C";
        CheckGameState();
    }
}
