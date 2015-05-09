using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SliderJoint2D))]

public class GrabElevator : GrabParent {

	public bool inverted;

	private SliderJoint2D slider;
	
	protected override void Initiate() {
		slider = GetComponent<SliderJoint2D> ();
	}
	
	protected override void GrabbedUpdate () {
		if (moveWith != moveType.custom)
			return;

		// Get the mouse location
		Vector2 rawMouse = Input.mousePosition;
		Vector2 mouse = Camera.main.ScreenToWorldPoint (rawMouse);
		
		// Vector from object to mouse;
		Vector2 deltaDist = mouse - rbody.position;
		
		if (Mathf.Abs (deltaDist.y) > 0) {
			JointMotor2D motor = slider.motor;
			motor.motorSpeed = Mathf.Clamp (-deltaDist.y/2 * speed, -speed, speed);
			slider.motor = motor;
		}
	}
	
	protected override void CustomUpdate () {
		if (!isGrabbed) {
			// Return to starting position
			JointMotor2D motor = slider.motor;
			motor.motorSpeed = speed / 4 * (inverted ? -1 : 1);
			slider.motor = motor;
		}
	}

}
