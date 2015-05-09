using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 8.5f;
	public float runSpeed = 12f;
	public float groundDamping;
	public float inAirDamping;

	[Space(12)]
	public float jumpVelocity = 13.5f;
	public float jumpFraction = 0.5f;
	public float jumpTime = 0.5f;

	[Space(12)]
	public Transform groundCheckTransform;
	public float groundCheckRadius;
	public LayerMask groundCheckLayerMask;

	[Space(12)]
	public bool canMove = true;
	public bool canJump = true;

	private float jumpTimer;

	private Animator anim;
	private PlayerHealth health;
	private Rigidbody2D rbody;

	private bool facingRight = true;
	private bool grounded = false;
	private bool running = false;

	void Awake () {
		// Get the components
		anim = GetComponent<Animator> ();
		health = GetComponent<PlayerHealth> ();
		rbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		// Check if the player is grounded
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, groundCheckRadius, groundCheckLayerMask);
		anim.SetBool ("Ground", grounded);

		if (health.IsDead ())
			return;

		// Change velocity
		AdditionalVelocity ();
		UserMovement ();

		// Send values to animator
		anim.SetFloat ("vSpeed", rbody.velocity.y * rbody.gravityScale);
	}

	void Update() {
		if (health.IsDead ())
			return;

		if (health.IsDamaged ())
			return;

		Jump ();
	}

	void Jump() {
		// Check for jumping
		if (Input.GetButtonDown ("Jump") && grounded && canJump) {
			// Cant jump while jumping
			grounded = false;
			// Set vertical velocity
			rbody.velocity = new Vector2(rbody.velocity.x, jumpVelocity);
			
			jumpTimer = jumpTime;
		}
		
		if (Input.GetButton ("Jump") && jumpTimer > 0 && canJump) {
			// Keep "jumping"
			rbody.velocity += new Vector2(0, jumpFraction * (jumpTimer/jumpTime));
			
			jumpTimer -= Time.deltaTime;
		}
	}

	void UserMovement() {
		if (!canMove)
			return;

		// Read input, normalized speed
		float normSpeed = Input.GetAxisRaw ("Horizontal");
		if (!canMove)
			normSpeed = 0.0f;
		
		// Check if the player wants to run
		running = Input.GetButton ("Sprint");
		float moveSpeed = running ? runSpeed : speed;

		// apply horizontal speed smoothing it
		float smoothedMovementFactor = grounded ? groundDamping : inAirDamping; // how fast do we change direction?
		rbody.velocity = new Vector2(
			Mathf.Lerp( rbody.velocity.x, normSpeed * moveSpeed, Time.deltaTime * smoothedMovementFactor )
		,	rbody.velocity.y
		);

		// Flip if nessesary
		if (normSpeed > 0 && !facingRight)
			Flip ();
		if (normSpeed < 0 && facingRight)
			Flip ();

		// Send values to animator
		anim.SetFloat ("Speed", Mathf.Abs (rbody.velocity.x));
		anim.SetBool ("Running", running);
	}

	void AdditionalVelocity() {
		Vector2 vel = rbody.velocity;
		foreach (FollowVelocityOnTrigger script in FindObjectsOfType<FollowVelocityOnTrigger>()) {
			script.VelocityUpdate(ref vel);
		}
		rbody.velocity = vel;
	}

	void Flip() {
		if (health.IsDamaged ())
			return;

		// Flip facing
		facingRight = !facingRight;

		// Get current scale
		Vector2 scale = transform.localScale;
		// Flip scale on the x axis
		scale.x *= -1;

		// Save
		transform.localScale = scale;
	}

	public void FlipTowards(float x) {
		x = Mathf.Sign (x - transform.position.x);

		if (x != Mathf.Sign(transform.localScale.x))
			Flip ();

	}

	public bool isGrounded() {
		return grounded;
	}
	public bool isGroundedRecheck() {
		return Physics2D.OverlapCircle (groundCheckTransform.position, groundCheckRadius, groundCheckLayerMask);
	}
}
