using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(TurtleMovement))]
[RequireComponent (typeof(Rigidbody2D))]

public class GrabTurtle : GrabParent {
	
	public float flipSpeed = 7f;

	public Collider2D[] disableOnGrab;
	public Collider2D[] disableOnAlt;

	private Animator anim;
	private TurtleMovement move;
	
	protected override void Initiate() {
		anim = GetComponent<Animator> ();
		move = GetComponent<TurtleMovement> ();
	}
	
	protected override void StartGrab() {

		anim.SetBool ("Grab", true);

		move.ResetTurning ();

		// Disable components
		DisableAll (disableOnGrab);
	}
	
	protected override void StopGrab() {
		anim.SetBool ("Grab", false);

		// Re-enable components
		EnableAll (disableOnGrab);
	}

	
	protected override void StartAlt() {
		rbody.isKinematic = true;
		rbody.velocity = new Vector2();

		// Disable components
		DisableAll (disableOnAlt);
	}
	
	protected override void StopAlt() {
		rbody.isKinematic = false;
		SwapLayer (11);

		// Re-enable components
		EnableAll (disableOnAlt);
	}

	protected override void CustomFixedUpdate() {
		if (isAlt) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (180f, Vector3.forward), Time.deltaTime * flipSpeed);
		} else {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (0, Vector3.forward), Time.deltaTime * flipSpeed);
		}
	}

	void EnableAll(Collider2D[] list) {
		if (list.Length > 0) {
			foreach (Collider2D comp in list) {
				comp.enabled = true;
			}
		}
	}
	void DisableAll(Collider2D[] list) {
		if (list.Length > 0) {
			foreach (Collider2D comp in list) {
				comp.enabled = false;
			}
		}
	}

}

