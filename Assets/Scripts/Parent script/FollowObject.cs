using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public GameObject target;
	
	private float zPosition;
	
	// Use this for initialization
	void Start () {
		// Fixed Z position
		zPosition = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		// Current location of object
		Vector3 targetPosition = target.transform.position;
		
		// Fixed Z position. Don't vary.
		targetPosition.z = zPosition;
		
		// Save
		transform.position = targetPosition;
	}
}
