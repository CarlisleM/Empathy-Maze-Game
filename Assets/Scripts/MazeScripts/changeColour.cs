using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class changeColour : MonoBehaviour {

	Image button;

	void Start() {
		button = GetComponent<Image>();
	}

	public void buttonDownChangeColor() {
		button.color = Color.gray;
	}

	public void buttonUpChangeColor() {
		button.color = Color.white;
	}

}
