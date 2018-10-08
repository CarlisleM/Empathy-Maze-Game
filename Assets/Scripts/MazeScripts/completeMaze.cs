using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class completeMaze : MonoBehaviour {

	public Texture completeMazeTexture;

	void OnGUI()
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), completeMazeTexture);
		if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 150, 25), "Continue")) {
			SceneManager.LoadScene("PostMazeLoading");
		}
	}
}