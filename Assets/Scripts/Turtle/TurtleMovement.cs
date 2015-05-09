using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GrabParent))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(Rigidbody2D))]

public class TurtleMovement : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;

	public Transform platformCheck;
	public float platformCheckRadius;

	public Transform groundCheck;
	public float groundCheckRadius;

	public LayerMask whatIsGround;

	private bool turning = false;
	private bool facingRight;
	private bool grounded;

	private GrabParent grab;
	private Animator anim;
	private Rigidbody2D rbody;

	void Awake() {
		grab = GetComponent<GrabParent> ();
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
	}

	void Start() {
		facingRight = transform.localScale.x > 0;
	}

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		// Check for empty ground below
		if (!Physics2D.OverlapCircle(platformCheck.position, platformCheckRadius, whatIsGround) && grounded) {
			// End of platform
			Turn ();
		}

		// Walk
		if (grounded && !grab.GetGrab() && !turning) {
			// Get current velocity
			Vector2 vel = rbody.velocity;

			// Move a precentage towards maxSpeed
			vel.x = Mathf.Lerp (vel.x, facingRight ? maxSpeed : -maxSpeed, acceleration);

			// Apply new speed
			rbody.velocity = vel;
		}
		
	}

	void Flip() {
		turning = false;

		if (!grab.GetGrab ()) {
			// Flip facing
			facingRight = !facingRight;

			// Get current scale
			Vector2 scale = transform.localScale;
		
			// Flip scale on the x axis
			scale.x *= -1;
		
			// Save
			transform.localScale = scale;
		}
	}

	public void ResetTurning() {
		turning = false;
	}

	public void Turn() {
		if (!turning) {
			anim.SetTrigger ("Turn");
			turning = true;
		}
	}

}
