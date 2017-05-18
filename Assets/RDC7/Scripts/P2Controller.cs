using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2Controller : MonoBehaviour {

    public Image []imgBanca;
    public Image []imgArbol;
    public Image imgFuente;
    public Image imgVereda;
    public Image imgBrillo;

    private GameObject btnNext; 

    public int waitTime = 1;
    // Use this for initialization
    void Start () {
        StartCoroutine("RutinaBrillos");
        Transform panelMenu = this.transform.FindChild("BottomMenu");
        btnNext = panelMenu.transform.FindChild("Next").gameObject;
        btnNext.SetActive(false);
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator RutinaBrillos()
    {
        
        yield return new WaitForSeconds(waitTime);
        Image brillo1 = CreateShine(imgBanca[0].transform);
        Image brillo2 = CreateShine(imgBanca[1].transform);
        yield return new WaitForSeconds(waitTime);
        Destroy(brillo1);
        Destroy(brillo2);
        yield return new WaitForSeconds(waitTime);
        Image brillo3 = CreateShine(imgArbol[0].transform);
        Image brillo4 = CreateShine(imgArbol[1].transform);
        yield return new WaitForSeconds(waitTime);
        Destroy(brillo3);
        Destroy(brillo4);
        yield return new WaitForSeconds(waitTime);
        Image brillo5 = CreateShine(imgVereda.transform);
        yield return new WaitForSeconds(waitTime);
        Destroy(brillo5);
        yield return new WaitForSeconds(waitTime);
        Image brillo6 = CreateShine(imgFuente.transform);
        yield return new WaitForSeconds(waitTime);
        Destroy(brillo6);
        yield return new WaitForSeconds(waitTime);

        btnNext.SetActive(true);

    }

    private Image CreateShine(Transform t) {
        Image tmp_image = (Image)Instantiate(imgBrillo);
        tmp_image.transform.SetParent(t);
        tmp_image.transform.position = new Vector2(t.position.x, t.position.y);
        tmp_image.transform.localScale = Vector3.one;
        return tmp_image;
    }
}
