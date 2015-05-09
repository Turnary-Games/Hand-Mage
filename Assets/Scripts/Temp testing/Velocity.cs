using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

	public Vector2 velocity;

	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = velocity;
	}
}
