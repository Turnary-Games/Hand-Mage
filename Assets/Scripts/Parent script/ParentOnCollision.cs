using UnityEngine;
using System.Collections;

public class ParentOnCollision : MonoBehaviour {

	public GameObject child;
	public bool active = false;

	private Rigidbody2D rbody;

	private Vector3 lastPosition;

	void Start () {
		lastPosition = transform.position;
	}
	
	void LateUpdate () {
		
		if (active) {
			Vector3 deltaDist = transform.position - lastPosition;
			child.transform.position += deltaDist;
		}
		
		lastPosition = transform.position;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject == child)
			active = true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject == child)
			active = false;
		
	}
}
