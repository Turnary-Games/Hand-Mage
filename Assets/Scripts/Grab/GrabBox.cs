using UnityEngine;
using System.Collections;

public class GrabBox : GrabParent {

	protected override void StartGrab() {

		GetComponent<Rigidbody2D>().fixedAngle = true;
	}
	
	protected override void StopGrab() {
		
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().fixedAngle = false;
	}
	
	
	protected override void StartAlt() {
		
		GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	protected override void StopAlt() {
		
		GetComponent<Rigidbody2D>().isKinematic = false;
		
		SwapLayer (11);
	}
	
}