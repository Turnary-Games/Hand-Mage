using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	public bool fixedZ = false;
	public float zValue = 0f;

	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (fixedZ) {
			Vector3 pos = transform.position;
			pos.z = zValue;
			transform.position = pos;
		}
	}
}
