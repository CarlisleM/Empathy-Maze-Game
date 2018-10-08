using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour {

	public Texture gameOverTexture;

	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),gameOverTexture); // Draws the texture based on the screen size

		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /3, 265, 25),"Retry")) { // Places button at location and displays text
			SceneManager.LoadScene("Maze1"); // Restart the level
		}

		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /3 + 25, 265, 25),"This is hard! Do you have any ideas REMI?")) {
			Application.Quit();
			// Insert REMI tip here?
		}
	}
}
