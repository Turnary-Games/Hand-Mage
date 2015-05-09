using UnityEngine;
using System.Collections;

public class GrabClockworkGear : GrabParent {

	public HingeJoint2D hinge;
	public ClockworkParent[] output;
	public bool invert = false;
	
	protected override void CustomUpdate() {
		
		if (output.Length > 0) {
			foreach (ClockworkParent clockwork in output) {
				if (GetAnalogBool())
					clockwork.SetStateUp();
				else
					clockwork.SetStateDown();
			}
		}

		if (moveWith != moveType.custom)
			return;

		if (isGrabbed) {
			JointMotor2D motor = hinge.motor;
			motor.motorSpeed = speed;
			hinge.motor = motor;
		} else {
			JointMotor2D motor = hinge.motor;
			motor.motorSpeed = -speed/2;
			hinge.motor = motor;
		}
	}
	
	public float GetPercentage() {
		float value = Mathf.InverseLerp (hinge.limits.min, hinge.limits.max, hinge.jointAngle);
		return invert ? 1.0f - value : value;
	}
	
	public bool GetAnalogBool() {
		return GetPercentage () == 1.0f;
	}
	
	public float GetAnalogNumber() {
		if (GetAnalogBool())
			return 1.0f;
		else
			return 0.0f;
	}
	
}

