using UnityEngine;
using System.Collections;

public class enemyRandomMove : MonoBehaviour {
	public int ranNum;
	public GameObject blinky;
	public float speed = 0.4f; // so we can modify the movement speed in inspector later
	Vector2 dest = Vector2.zero; // store movement destination

	// Use this for initialization
	void Start () {
		dest = transform.position;		
		ranNum = 1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(p);

		ranNum = Random.Range (1, 5);
		switch (ranNum) {
		case 1:
			if (valid (Vector2.up))
				dest = (Vector2)transform.position + Vector2.up*2;
			break;
		case 2:
			if (valid (-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up*2;
			break;
		case 3:
			if (valid (Vector2.right))
				dest = (Vector2)transform.position + Vector2.right*2;
			break;
		case 4:
			if (valid (-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right * 2;
			break;

		}

		// Calculate current movement direction and set animation parameters
		Vector2 dir = dest - (Vector2)transform.position; // Calculates movement direction by subtracting current position from destination position
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	// Checks if character can move to a square or not
	bool valid(Vector2 dir) {
		// Cast Line from 'next to character' to 'character'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>()); //  GetComponent to access characters's Rigidbody component
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "walls")
			ranNum = Random.Range (1, 5);
	}
}  