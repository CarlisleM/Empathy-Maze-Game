using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class toggleGUI : MonoBehaviour {

	bool audioState;

	void Start() {
		audioState = true;
	}

	public void toggleSound() {
		audioState = !audioState; // toggles audioState between on and off
		if (audioState){
			AudioListener.volume = 1F;
		} else {
			AudioListener.volume = 0F;
		}
	}

	// Sets up coroutine timer
	IEnumerator reloadScene() {
		float fadeTime = GameObject.Find ("MainCamera").GetComponent<fadeSceneTransition> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
		
	public void retryLevel() {
		StartCoroutine("reloadScene");
	}

}

