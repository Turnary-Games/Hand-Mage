using UnityEngine;
using System.Collections;

public class BasicRotator : MonoBehaviour {

	public Vector3 deltaRotation;
	
	// Update is called once per frame
	void Update () {
		Quaternion rotation = transform.rotation;
		rotation.eulerAngles += deltaRotation * Time.deltaTime;
		//rotation.eulerAngles.z %= 360;
		transform.rotation = rotation;
	}
}
