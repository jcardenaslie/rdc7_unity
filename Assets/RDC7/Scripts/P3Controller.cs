using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3Controller : MonoBehaviour {

    public string[] answer;
    //Paneles de ejercicios
    public RectTransform[] exercises;


    private string currentChoice;
    private int currentExcercise = 0;
    private GameObject btnNext;
    private int lastIndexExcercise;

    // Use this for initialization
    void Start () {
        DeactivateNextButton();
        exercises[currentExcercise].gameObject.SetActive(true);
        lastIndexExcercise = exercises.Length - 1;
	}
	

    private void DeactivateNextButton()
    {
        Transform panelMenu = this.transform.FindChild("BottomMenu");
        btnNext = panelMenu.transform.FindChild("Next").gameObject;
        btnNext.SetActive(false);
    }

    private void CheckGameState() {
        if (currentChoice == answer[currentExcercise] && currentExcercise < lastIndexExcercise)
        {
            Debug.Log("Right Choice Sgte ejercicio");
            exercises[currentExcercise].gameObject.SetActive(false);
            currentExcercise++;
            exercises[currentExcercise].gameObject.SetActive(true);
        }
        else if (currentChoice == answer[currentExcercise] && currentExcercise == lastIndexExcercise) {
            btnNext.SetActive(true);
            Debug.Log("Right Choice Fin de los ejercicios");
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
