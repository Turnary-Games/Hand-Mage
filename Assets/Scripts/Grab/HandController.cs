using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public Transform player;
	public Transform idlePos;
	public Transform mouse;

	public float speedAway = 8f;
	public float speedHome = 4f;
	public float minRadius = 4f;

	public float rotateSpeed = 7f;

	private Animator anim;
	private PlayerHealth health;
	[HideInInspector] public Transform target;
	[HideInInspector] public Vector3 lastPosition;

	public enum States { idle, waiting, moving, grabbed }
	private States state = States.idle;

	void Awake() {
		// Get the components
		anim = GetComponent<Animator> ();
		health = player.gameObject.GetComponent<PlayerHealth> ();
	}

	void Start() {
		lastPosition = transform.position;
	}

	void Update() {
		if ((Input.GetMouseButtonUp (0) && target == mouse) || health.IsDead()) {
			ResetTarget();
		}

		// If there is a target
		if (target != null) {
			// Go to target
			if (state == States.moving || state == States.waiting) {

				/*
				Vector2 tpos = new Vector2(target.position.x, target.position.y);
				Vector2 delta = tpos - rbody.position;

				rbody.AddForce(delta * Time.deltaTime * speedAway);
				*/
				transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * speedAway + 0.3f);

				// If the mouse is "close enough"
				if (target == mouse && Vector2.Distance(transform.position, target.position) < 0.5f) {
					// Wait for an object to pass by
					state = States.waiting;

					anim.SetBool ("Waiting", true);
					anim.SetBool ("Grabbed", false);
				} else {
					anim.SetBool ("Waiting", false);
				}

				// If the object is "close enough"
				if (target != mouse && Vector2.Distance(transform.position, target.position) < 0.25f) {
					// Grab the object
					state = States.grabbed;

					anim.SetBool ("Grabbed", true);
					anim.SetBool ("Waiting", false);
				}
			}

			// Jump to target, i.e. follow it
			if (state == States.grabbed) {
				transform.position = target.position;

				if (target.gameObject.name != "box") {
					// Rotate to grabbed object

					// Lerp
					//transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * flipSpeed);

					// Instantly
					transform.rotation = target.rotation;
				}

				// Flip
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Abs(scale.x);
				transform.localScale = scale;

				anim.SetBool ("Waiting", false);
			}

		} else {
			// Rotate back to idle 
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * rotateSpeed);

			if (Input.GetMouseButton(0) && !health.IsDead()) {
				// Have hand wait for an object to land in its palm
				SetTarget(mouse);
			} else {
				// Go to idle location
				transform.position = Vector3.Lerp(transform.position, idlePos.position, Time.deltaTime * speedHome);

				anim.SetBool ("Waiting", false);
			}
		}

		// Flip like the player
		if (transform.position.x != lastPosition.x && Mathf.Abs(transform.position.x - lastPosition.x) > 0.1f) {

			// Exceptions
			if (state == States.grabbed && target.name == "cog handle") {} else {
				// Flip
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Abs(scale.x) * Mathf.Sign(transform.position.x - lastPosition.x);
				transform.localScale = scale;
			}
		}
		lastPosition = transform.position;

	}

	void OnTriggerEnter2D(Collider2D other) {
		ColliderCheck (other);
	}
	void OnTriggerStay2D(Collider2D other) {
		ColliderCheck (other);
	}

	void ColliderCheck(Collider2D other) {
		if (state == States.waiting && other.tag == "Grabable") {
			// Get the grab component
			GrabParent grabComp = other.GetComponent<GrabParent>();

			// If it aint in the main object then check the parent
			if (grabComp == null) {
				grabComp = other.GetComponentInParent<GrabParent>();
			}

			// in any of the cases, target it.
			if (grabComp) {
				SetTarget(other.transform, true);
				
				grabComp.ParentStartGrab();
			}
		}
	}

	public void SetTarget(GameObject t, bool direct = false) {
		SetTarget (t.transform, direct);
	}

	public void SetTarget(Transform t, bool direct = false) {
		target = t;

		state = direct ? States.grabbed : States.moving;

		anim.SetBool ("Traveling", true);

		if (direct) {
			anim.SetBool("Grabbed", true);
		}
	}

	public void ResetTarget() {
		target = null;
		state = States.idle;

		anim.SetBool ("Traveling", false);
		anim.SetBool ("Grabbed", false);
	}

	public States GetState() {
		return state;
	}
	
	public void SetState(States newState) {
		state = newState;
	}
	
	public bool IsGrabbed() { return state == States.grabbed; }
	public bool IsIdle() { return state == States.idle; }
	public bool IsMoving() { return state == States.moving; }
	public bool IsWaiting() { return state == States.waiting; }

}
