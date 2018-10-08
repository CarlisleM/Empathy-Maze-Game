using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class checkPoint : MonoBehaviour {

	bool cpReached;
	int sceneCheck = 1;

	// Use this for initialization
	void Start () {
		cpReached = false;
	}


	public void reachPoint() {
		cpReached = true;
	}
		
	// Sets up coroutine timer
	IEnumerator reloadScene() {
		yield return new WaitForSeconds(1.5f); // Wait for animation/audio to stop playing
		float fadeTime = GameObject.Find ("MainCamera").GetComponent<fadeSceneTransition> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("scene" + sceneCheck);
	}
		

	void update() {
		// inside death trigger
		if (cpReached) {
			sceneCheck++;
		}

		StartCoroutine("reloadScene");
		// inside death trigger
	}

}
	
