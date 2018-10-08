using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class heroMove : MonoBehaviour {
	public float speed = 0.4f; // so we can modify the movement speed in inspector later
	public AudioClip eyesCollected;
	private AudioSource audioSource;
	Vector2 dest = Vector2.zero; // store movement destination

	// Used to check if the UI touch button is being held down
	bool moveUp, moveDown, moveRight, moveLeft;

	// Use this for initialization
	void Start () {
		dest = transform.position;
		moveUp = false;
		moveDown = false;
		moveLeft = false;
		moveRight = false;
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(p);

		if (moveUp == true) {
			Up ();
		} else if (moveDown == true) {
			Down();
		} else if (moveLeft == true) {
			Left ();
		} else if (moveRight == true) {
			Right();
		}

		// Check for Input if not moving
		// We are not moving if the current position equals the destination
		if ((Vector2)transform.position == dest) {
			if (Input.GetKey (KeyCode.UpArrow)) 
				Up ();
			if (Input.GetKey (KeyCode.RightArrow))
				Right ();
			if (Input.GetKey (KeyCode.DownArrow))
				Down ();
			if (Input.GetKey (KeyCode.LeftArrow)) 
				Left ();
		}
			
		// Calculate current movement direction and set animation parameters
		Vector2 dir = dest - (Vector2)transform.position; // Calculates movement direction by subtracting current position from destination position
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}
		
	// Movement functions
	// Move Right
	public void Right() {
		if (((Vector2)transform.position == dest) && valid (Vector2.right))
			dest = (Vector2)transform.position + Vector2.right*2;
	}

	public void MoveRightTrue() {
		moveRight = true;
	}

	public void MoveRightFalse() {
		moveRight = false;
	}

	// Move Left
	public void Left() {
		if (((Vector2)transform.position == dest) && valid (-Vector2.right))
			dest = (Vector2)transform.position - Vector2.right * 2;
	}

	public void MoveLeftTrue() {
		moveLeft = true;
	}

	public void MoveLeftFalse() {
		moveLeft = false;
	}
		
	// Move Up
	public void Up() {
		if (((Vector2)transform.position == dest) && valid (Vector2.up))
			dest = (Vector2)transform.position + Vector2.up*2;
	}

	public void MoveUpTrue() {
		moveUp = true;
	}

	public void MoveUpFalse() {
		moveUp = false;
	}

	// Move Down
	public void Down() {
		if (((Vector2)transform.position == dest) && valid (-Vector2.up))
			dest = (Vector2)transform.position - Vector2.up*2;
	}

	public void MoveDownTrue() {
		moveDown = true;
	}

	public void MoveDownFalse() {
		moveDown = false;
	}
		
	// Checks if character can move to a square or not
	bool valid(Vector2 dir) {
		// Cast Line from 'next to character' to 'character'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>()); //  GetComponent to access characters's Rigidbody component
	}

	// Sets up coroutine timer
	IEnumerator LoadNextScene() {
		yield return new WaitForSeconds(1.75f); // Wait for animation/audio to stop playing
		float fadeTime = GameObject.Find ("MainCamera").GetComponent<fadeSceneTransition> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("PostMazeGame");
	}

	// Destroy character upon touching, play character death sound, restart the level
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "eyeSprite") {
			Destroy(co.gameObject); // Destroy robot eye sprite

			// Disable hero and enemy movement
			if (Application.loadedLevelName == "Maze1") {
				GameObject.Find ("hero").GetComponent<heroMove> ().enabled = false;
			}
			if (Application.loadedLevelName == "Maze2") {
				GameObject.Find ("hero2").GetComponent<heroMove> ().enabled = false;
			}
			if (Application.loadedLevelName == "Maze3") {
				GameObject.Find ("hero3").GetComponent<heroMove> ().enabled = false;
			}
			GameObject.Find ("enemy1").GetComponent<enemyRandomMovement> ().enabled = false;
			GameObject.Find ("enemy2").GetComponent<enemyRandomMovement> ().enabled = false;
			GameObject.Find ("enemy3").GetComponent<enemyRandomMovement> ().enabled = false;
			GameObject.Find ("enemy4").GetComponent<enemyRandomMovement> ().enabled = false;

			// Stop the background music from playing
			GameObject soundObject = GameObject.Find("MainCamera");
			AudioSource backgroundMusic = soundObject.GetComponent<AudioSource>();
			backgroundMusic.mute = !backgroundMusic.mute;

			// Play the sound clip for collecting the robot eyes
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = eyesCollected;
			audioSource.Play();
			StartCoroutine("LoadNextScene");
		}
	}
}