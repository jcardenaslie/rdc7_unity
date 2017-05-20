using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColor : MonoBehaviour {

    private Color color;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        Image tmp_image = GetComponent<Image>();
        //Debug.Log("color imagen" + tmp_image.color);
        this.color = tmp_image.color;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    public Color GetColor() {
        return this.color;
    }

    public void RestartPosition() {
        transform.position = startPosition;
    }
}
