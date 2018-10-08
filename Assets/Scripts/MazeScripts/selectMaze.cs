using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class selectMaze : MonoBehaviour {

	public Texture previousScene;
	int random; 

	// Sets up coroutine timer
	IEnumerator LoadNextScene() {
		random = Random.Range (1, 4);
		Debug.Log ("random is: " + random);
		float fadeTime = GameObject.Find ("MainCamera").GetComponent<fadeSceneTransition> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene("Maze" + random); // Load maze
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),previousScene); // Draws the texture based on the screen size

		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /3, 265, 25),"Start maze")) { // Places button at location and displays text
			Debug.Log ("random is: " + random);
			StartCoroutine("LoadNextScene");
		}

		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /3 + 25, 265, 25),"This is hard! Do you have any ideas REMI?")) {
			Application.Quit();
			// Insert REMI tip here?
		}
	}
}
