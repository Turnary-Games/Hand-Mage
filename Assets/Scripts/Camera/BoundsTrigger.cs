using UnityEngine;
using System.Collections;

public class BoundsTrigger : MonoBehaviour {
	
	public Option onTrigger;
	public bool triggerOnStart = false;

	public bool zoom = false;
	public float zoomAmount;

	public BoxCollider2D bounds;


	void Start () {
		if (triggerOnStart)
			SetBounds ();
	}
	
	void SetBounds() {
		CameraControl cam = FindObjectOfType<CameraControl> ();
		// Bounds
		cam.SetBounds (bounds.bounds);
		// Zoom
		if (zoom) cam.SetZoom (zoomAmount);
		else cam.DisableZoom ();
	}

	void DisableBounds() {
		FindObjectOfType<CameraControl> ().DisableBounds ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			// Trigger
			if (onTrigger == Option.setBounds)
				SetBounds();
			else if (onTrigger == Option.disableBounds)
				DisableBounds();
		}
	}

	void OnDrawGizmos() {
		if (triggerOnStart) {
			// Draw bounds
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube (bounds.bounds.center, bounds.bounds.size);
		}
	}

	void OnDrawGizmosSelected() {
		if (zoom) {
			// Draw camera size representation
			Camera cam = Camera.main;
			Gizmos.color = Color.cyan;

			Gizmos.DrawWireCube (bounds.bounds.center, new Vector3 (cam.aspect, 1) * zoomAmount * 2);
		}

		if (!triggerOnStart) {
			// Draw bounds
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube (bounds.bounds.center, bounds.bounds.size);
		}
	}

	public enum Option { setBounds, disableBounds };
}
