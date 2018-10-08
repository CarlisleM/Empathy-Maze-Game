using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class changeSprite : MonoBehaviour {

	public Sprite sprite1; 
	public Sprite sprite2; 

	bool spriteToggle;

	void Start () {
		spriteToggle = true;
	}

	// Changes the sprite
	public void switchSprite () {
		spriteToggle = !spriteToggle; // toggles the sprite
		if (spriteToggle) {
			gameObject.GetComponent<Image> ().sprite = sprite1;
		} else {
			gameObject.GetComponent<Image> ().sprite = sprite2;
		}
	}
}

