using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public GameObject target;
	public float xOffset = 0;
	public float yOffset = 0;
	public float zOffset = 0;

	void LateUpdate() {   
		if (target != null) {
			this.transform.position = new Vector3 (target.transform.position.x + xOffset,
				target.transform.position.y + yOffset,
				target.transform.position.z + zOffset);
		}
	}
}
