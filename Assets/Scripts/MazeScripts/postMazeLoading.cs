using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class postMazeLoading : MonoBehaviour {

	public Texture loadingScreen;

	void Start() {
		StartCoroutine("LoadNextScene");
	}

	// Sets up coroutine timer
	IEnumerator LoadNextScene() {
		yield return new WaitForSeconds(5f); // Wait for animation/audio to stop playing
		float fadeTime = GameObject.Find ("MainCamera").GetComponent<fadeSceneTransition> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("PostMazeGame");
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),loadingScreen); // Draws the texture based on the screen size
	}
		
}

