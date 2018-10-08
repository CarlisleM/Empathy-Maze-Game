using UnityEngine;
using System.Collections;

public class displayInstructions : MonoBehaviour {
	
	bool instructionsImage = false;
	GameObject[] pauseObjects;
	public GameObject GameSpecificTouchElements;

	void Start () {
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
	}

	public void toggleInstructions() {
		instructionsImage = !instructionsImage;
		if (instructionsImage) {
			showPaused ();
			GameSpecificTouchElements.gameObject.SetActive(false);
		} else {
			hidePaused ();
			GameSpecificTouchElements.gameObject.SetActive(true);
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
		Time.timeScale = 0;
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios)
		aud.Pause ();
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
		Time.timeScale = 1;
		AudioSource[] audios = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource aud in audios)
		aud.UnPause ();
	}
}
