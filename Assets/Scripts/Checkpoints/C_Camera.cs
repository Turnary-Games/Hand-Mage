using UnityEngine;
using System.Collections;

public class C_Camera {

	Vector3 position;
	Bounds bounds;
	float zoomAmount;
	
	public C_Camera () {
		// Find camera instance
		CameraControl cam = CameraControl.FindObjectOfType<CameraControl> ();

		position = cam.transform.position;
		bounds = cam.bounds;
		zoomAmount = cam.zoomAmount;
	}

	public void Load() {
		// Find camera instance
		CameraControl cam = CameraControl.FindObjectOfType<CameraControl> ();

		cam.transform.position = position;
		cam.bounds = bounds;
		cam.zoomAmount = zoomAmount;
	}
}
