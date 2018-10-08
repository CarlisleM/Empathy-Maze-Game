using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enemyRandomMovement : MonoBehaviour {

	public float speed = 0.4f; // Used to modify the movement speed in Unity's inspector
	Vector2 dest = Vector2.zero; // Store movement destination

	public AudioClip heroKilled; // Targets audio file to be played when triggered
	private AudioSource audioSource;

	int [] validMoves = new int[4] {0, 0, 0, 0}; // { PreviousMove, Up, Down, Left, Right }
	int arrayLength;

	int moveDecider; // Generates random number to randomise movement
	int moveDecision; // stores enemy movement decision
	int oppositePrevious;
	int option1;
	int option2;
	int option3;
	int option4;

	int option1Counter;
	int option2Counter;
	int option3Counter;
	int option4Counter;

	// Initialization
	void Start () {
		dest = transform.position;
	}	

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(p);

//--------------------------------------------------------------------------------------------------------------------------//
		// If the character is not moving (it has completed its previous movement), perform next action
		if ((Vector2)transform.position == dest) {

			// Reset all values to 0
			validMoves = new int[4]  {0, 0, 0, 0};
			arrayLength = 0;
			option1 = 0;
			option2 = 0;
			option3 = 0;

			// Determine valid moves and sets options accordingly
			setValidMoveArray();	// Changes array values [0-4] to 1 if move is valid 
			countValidMoves();		// Counts how many valid moves can be made and stores in arrayLength
			decideMoveOptions();	// Sets move options1, 2, 3 and 4
			// 0 0 1 1		arrayLength = 2		option1 = 3	(Left)	Option2 = 4 (Right)
			moveDecider = Random.Range (1, 101);	// randomly generate a number between 1-100 to decide odds

			if (arrayLength == 1) {		// if one valid move
				moveDecision = oppositePrevious;
			} else if (arrayLength == 2) {		// if two valid moves
				if (moveDecider < 6) {				
					moveDecision = oppositePrevious;
				} else {
					if (oppositePrevious == option1) {
						moveDecision = option2; 								
					} else {
						moveDecision = option1;
					}				
				}

				// 1 1 1 0	option1 = up	option2 = down		option3 = left
				// 1 0 1 1	option1 = up	option2 = left		option3 = right
				// 1 1 0 1	option1 = up	option2 = down		option3 = right
				// 0 1 1 1	option1 = down	option2 = left		option3 = right

				// moving up = 1		opposite = 2
				// moving down = 2		opposite = 1
				// moving left = 3		opposite = 4
				// moving right = 4		opposite = 3
//--------------------------------------------------------------------------------------------------------------------------//
			} else if (arrayLength == 3) {		// if three valid moves
				if (moveDecider < 6) {
					moveDecision = oppositePrevious;
				} else if (moveDecider > 10 && moveDecider < 56) {
					if (option1 != oppositePrevious) {
						moveDecision = option1;
					} else if (option2 != oppositePrevious) {
						moveDecision = option2;
					}
				} else {
					if (option2 != oppositePrevious) {
						moveDecision = option2;
					} else if (option3 != oppositePrevious) {
						moveDecision = option3;
					}
				}
//			} else if (arrayLength == 4) {		// if four valid moves
//				if (moveDecider < 6) {
//					moveDecision = oppositePrevious;
//				} else if (moveDecider > 10 && moveDecider < 41) {
//					if (option1 != oppositePrevious) {
//						moveDecision = option1;
//					} else if (option2 != oppositePrevious) {
//						moveDecision = option2;
//					}
//				} else if (moveDecider > 40 && moveDecider < 71) {
//					if (option2 != oppositePrevious) {
//						moveDecision = option2;
//					} else if (option3 != oppositePrevious) {
//						moveDecision = option3;
//					}
//				} else {
//					if (option2 != oppositePrevious) {
//						moveDecision = option3;
//					}
//				}
			} 
				
			switch (moveDecision) {
			case 1:
				Up ();
				break;
			case 2:
				Down ();
				break;
			case 3:
				Left ();
				break;
			case 4:
				Right ();
				break;
			}

			oppositeDirection();	// Stores the direction opposite of the previous move

			if (arrayLength == 3) {
				if (moveDecision == option1) {
					option1Counter++;
				} else if (moveDecision == option2) {
					option2Counter++;
				} else if (moveDecision == option3) {
					option3Counter++;
				}
			}

		//	Debug.Log("moveDecision = " + moveDecision + "   random number = " + moveDecider  + "   Previous Move = " + moveDecision + "   Opposite Previous Move: " + oppositePrevious + "   Up: " + validMoves[0] + " Down: " + validMoves[1] + " Left: " + validMoves[2] + " Right: " + validMoves[3] + "   Number of valid moves = " + arrayLength  + "   Option1 = " + option1 + " Option2 = " + option2 + " Option3 = " + option3 + " option1Counter = " + option1Counter + " option2Counter = " + option2Counter + " option3Counter = " + option3Counter);
		}
	
		// Calculate current movement direction and set animation parameters
		Vector2 dir = dest - (Vector2)transform.position; // Calculates movement direction by subtracting current position from destination position
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);

	}

	// If the unit to the right of the character is valid, move the character right 1 unit
	public void Right() {
		if (valid (Vector2.right))
			dest = (Vector2)transform.position + Vector2.right*2;
	}

	// If the unit to the left of the character is valid, move the character left 1 unit
	public void Left() {
		if (valid (-Vector2.right))
			dest = (Vector2)transform.position - Vector2.right * 2;
	}

	// If the unit to the up of the character is valid, move the character up 1 unit
	public void Up() {
		if (valid (Vector2.up))
			dest = (Vector2)transform.position + Vector2.up*2;
	}

	// If the unit to the down of the character is valid, move the character down 1 unit
	public void Down() {
		if (valid (-Vector2.up))
			dest = (Vector2)transform.position - Vector2.up*2;
	}

	// Counts how many valid moves can be made
	public void countValidMoves() {
		for (int i = 0; i < 4; i++) {
			if (validMoves[i] == 1) {
				arrayLength = arrayLength+1;
			}
		}
	}

	public void setValidMoveArray() {
		if (valid (Vector2.up))  {
			validMoves[0] = 1;		// set 1 if up is valid
		}

		if (valid (-Vector2.up)) {
			validMoves[1] = 1;		// set 1 if down is valid
		}

		if (valid (-Vector2.right)) {
			validMoves[2] = 1;		// set 1 if left is valid
		}

		if (valid (Vector2.right)) {
			validMoves[3] = 1;		// set 1 if right is valid
		}
	}
		
	public void decideMoveOptions() {
		if (validMoves[0] == 1) {
			option1 = 1;
		}

		if (validMoves[1] == 1) {
			if (option1 == 0) {
				option1 = 2;
			} else if (option2 == 0) {
				option2 = 2;
			}
		}

		if (validMoves[2] == 1)  {
			if (option1 == 0) {
				option1 = 3;
			} else if (option2 == 0) {
				option2 = 3;
			} else if (option3 == 0) {
				option3 = 3;
			}
		}

		if (validMoves[3] == 1) {
			if (option1 == 0) {
				option1 = 4;
			} else if (option2 == 0) {
				option2 = 4;
			} else if (option3 == 0) {
				option3 = 4;
			}
		}
	}

	public void oppositeDirection() {
		if (moveDecision == 1) {
			oppositePrevious = 2;
		} else if (moveDecision == 2) {
			oppositePrevious = 1;
		} else if (moveDecision == 3) {
			oppositePrevious = 4;
		} else if (moveDecision == 4) {
			oppositePrevious = 3;
		} else {
			// Do nothing
		}
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
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	// Destroy character upon touching, play character death sound, restart the level
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "hero" || co.name == "hero2" || co.name == "hero3") {
			Destroy(co.gameObject); // Destroy character
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = heroKilled;
			audioSource.Play();
			StartCoroutine("LoadNextScene");
		}
	}
}
