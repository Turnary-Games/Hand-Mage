using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SliderJoint2D))]
public class ClockworkSlide : ClockworkParent {

	public bool invert = false;

	private SliderJoint2D slider;

	protected override void Initiate() {
		slider = GetComponent<SliderJoint2D> ();
	}

	protected override void CustomUpdate () {
		JointMotor2D motor = slider.motor;


		motor.motorSpeed = state == States.up ? motorSpeed : -motorSpeed;
		if (invert) motor.motorSpeed *= -1;

		if (invert) {
			if ((state == States.up && slider.limitState == JointLimitState2D.LowerLimit) 
				|| (state == States.down && slider.limitState == JointLimitState2D.UpperLimit)) {
				motor.motorSpeed = 0;
			}
		} else {
			if ((state == States.up && slider.limitState == JointLimitState2D.UpperLimit) 
			    || (state == States.down && slider.limitState == JointLimitState2D.LowerLimit)) {
				motor.motorSpeed = 0;
			}
		}

		slider.motor = motor;
	}

}
