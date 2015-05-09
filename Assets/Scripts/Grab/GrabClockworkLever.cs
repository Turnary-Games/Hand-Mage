using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(HingeJoint2D))]

public class GrabClockworkLever : GrabParent {
	
	public ClockworkParent[] output;
	public bool invert = false;

	private HingeJoint2D hinge;


	protected override void Initiate() {
		// Get component
		hinge = GetComponent<HingeJoint2D> ();
	}

	protected override void CustomUpdate() {

		if (output.Length > 0) {
			foreach (ClockworkParent clockwork in output) {
				if (GetAnalogBool())
					clockwork.SetStateUp();
				else
					clockwork.SetStateDown();
			}
		}

	}

	public float GetPercentage() {
		float value = Mathf.InverseLerp (hinge.limits.min, hinge.limits.max, hinge.jointAngle);

		return invert ? 1.0f - value : value;
	}

	public bool GetAnalogBool() {
		return GetPercentage () > 0.5f;
	}

	public float GetAnalogNumber() {
		if (GetAnalogBool())
			return 1.0f;
		else
			return 0.0f;
	}

}

