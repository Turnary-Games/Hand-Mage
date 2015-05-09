using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public GameObject target;
	public float multiplier = 1;
	public float offset;
	public Axis axis;

	[Space(20)]
	public bool workWithin;
	public float min;
	public float max;

	public enum Axis { horizontal, veritcal, rotation }

	void LateUpdate () {
		switch (axis) {
		case Axis.veritcal:
			// Y-axis
			transform.rotation = Quaternion.AngleAxis ((target.transform.position.y + offset) * multiplier, Vector3.forward);
			break;

		case Axis.horizontal:
			// X-axis
			transform.rotation = Quaternion.AngleAxis ((target.transform.position.x + offset) * multiplier, Vector3.forward);
			break;

		case Axis.rotation:
			// Rotation Z-axis
			transform.rotation = Quaternion.AngleAxis ((target.transform.rotation.eulerAngles.z + offset) * multiplier, Vector3.forward);
			break;
		}
	}
}
