using UnityEngine;
using System.Collections;

public class ParentOnTrigger: MonoBehaviour {
	
	public Rigidbody2D child;
	public bool active = false;
	
	private Vector3 lastPosition;
	
	void Start () {
		lastPosition = transform.position;
	}
	
	void LateUpdate () {
		
		if (active) {
			// Add distance differance
			child.transform.position += transform.position - lastPosition;
		}
		
		lastPosition = transform.position;
	}
	
	// Activate
	void OnTriggerEnter2D(Collider2D other) {
		if (other.attachedRigidbody == child)
			active = true;
	}
	
	// Deactivate
	void OnTriggerExit2D(Collider2D other) {
		if (other.attachedRigidbody == child)
			active = false;
		
	}
}
