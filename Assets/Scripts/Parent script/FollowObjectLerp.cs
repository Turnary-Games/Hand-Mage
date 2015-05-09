using UnityEngine;
using System.Collections;

public class FollowObjectLerp : MonoBehaviour {

	public GameObject target;
	public float cameraSpeed = 2f;

	public bool zoom = true;
	public float minSize = 3;
	public float cameraZoomSpeed = 0.5f;

	private float zPosition;

	// Use this for initialization
	void Start () {
		// Fixed Z position
		zPosition = transform.position.z;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// Current location of objects
		Vector3 cameraPosition = transform.position;
		Vector3 targetPosition = target.transform.position;

		// New location is only part of the distance (not 100% at the targets location)
		Vector3 newPosition = Vector3.Lerp (cameraPosition, targetPosition, Time.deltaTime * cameraSpeed);

		// Fixed Z position. Don't vary.
		newPosition.z = zPosition;

		// Save
		transform.position = newPosition;


		if (zoom) {
			// The size in relation to how fast the player is moving
			float speed = target.GetComponent<Rigidbody2D>().velocity.magnitude;
			GetComponent<Camera>().orthographicSize = Mathf.Lerp (GetComponent<Camera>().orthographicSize, minSize + speed, Time.deltaTime * cameraZoomSpeed);
		}
	}
}
