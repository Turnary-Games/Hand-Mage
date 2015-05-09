using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SliderJoint2D))]

public class GrabSlider : GrabParent {

	public enum Axis { horizontal, vertical, both }
	public Axis axis;

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

		switch (axis) {
		case Axis.vertical:
			// Y-axis
			if (Mathf.Abs (deltaDist.y) > 0) {
				JointMotor2D motor = slider.motor;
				motor.motorSpeed = Mathf.Clamp (-deltaDist.y/2 * speed, -speed, speed);
				slider.motor = motor;
			}
			break;
		
		case Axis.horizontal:
			// X-axis
			if (Mathf.Abs (deltaDist.x) > 0) {
				JointMotor2D motor = slider.motor;
				motor.motorSpeed = Mathf.Clamp (-deltaDist.x/2 * speed, -speed, speed);
				slider.motor = motor;
			}
			break;

		case Axis.both:
			// Both X- and Y-axis
			if (deltaDist.magnitude > 0) {
				JointMotor2D motor = slider.motor;
				motor.motorSpeed = Mathf.Clamp (-deltaDist.x/2 * speed -deltaDist.y/2 * speed, -speed, speed);
				slider.motor = motor;
			}
			break;
		}
	}

	protected override void StopGrab () {
		// Reset the motor speed
		JointMotor2D motor = slider.motor;
		motor.motorSpeed = 0;
		slider.motor = motor;
	}
	
}
