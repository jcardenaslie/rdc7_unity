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

    public int waitTime = 1;
    // Use this for initialization
    void Start () {
        StartCoroutine("Example");	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Example()
    {
        Debug.Log("Time " + Time.time);

        Image tmp_brillo = (Image)
                        Instantiate(imgBrillo);

        tmp_brillo.transform.SetParent(imgBanca[0].transform);
        //Instantiate(imgBrillo, 
        //    new Vector2(imgBanca[0].rectTransform.position.x, 
        //        imgBanca[0].rectTransform.position.y), 
        //    Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        
    }
}
