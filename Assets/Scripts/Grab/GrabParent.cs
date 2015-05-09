using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class GrabParent : MonoBehaviour {

	// Public variables
	public static LayerMask playerLayerMask = 1 << 8; // wait til empty, including the objects in this layer

	public bool enableAlt = true;

	public enum moveType {velocity,force,forceAtPosition,custom};
	public moveType moveWith;

	public float speed = 4f;

	// objects that should swap layer.
	public GameObject[] swapsLayer; // Should always include the grabbable object
	public SpriteRenderer[] spriteRenderer; // everything that changes color

	// Protected variables
	protected float maxSpeed = 10f;

	protected bool isGrabbed = false;
	protected bool isAlt = false;

	protected Color grabColor = new Color (1, 1, 0.5f);
	protected Color altColor = new Color (0.7f, 0.7f, 0.7f);

	protected Rigidbody2D rbody;

	// Private variables
	private float originalMass;
	private HandController hand;

	void Awake() {
		// Get the hand controller script
		hand = GameObject.FindGameObjectWithTag ("Hand").GetComponent<HandController> ();

		// Get the components
		rbody = GetComponent<Rigidbody2D> ();

		// Original mass
		originalMass = rbody.mass;

		Initiate ();
	}

	protected virtual void Initiate() {
		// Nofin
	}

	void LateUpdate() {

		// Stop grab
		if (Input.GetMouseButtonUp (0)) {
			ParentStopGrab();
		}

		// Lock
		if (!isAlt && Input.GetMouseButtonDown(1) && isGrabbed) {
			ParentStartAlt();
		}

		// Unlock
		if (isAlt && ( Input.GetMouseButtonUp(1) || !isGrabbed )) {
			ParentStopAlt();
		}

		if (isGrabbed) {
			GrabbedUpdate ();

			// Move towards the mouse
			if (!isAlt) {
				
				// Get the mouse location
				Vector2 rawMouse = Input.mousePosition;
				Vector2 mouse = Camera.main.ScreenToWorldPoint (rawMouse);
				
				// Vector from object to mouse;
				Vector2 deltaDist = mouse - new Vector2 (transform.position.x, transform.position.y);

				switch(moveWith) {
				case moveType.force:
					// Force to be used
					rbody.AddForce(deltaDist * speed * Time.deltaTime);
					break;

				case moveType.forceAtPosition:
					// Force to be used
					rbody.AddForceAtPosition(deltaDist * speed * Time.deltaTime,transform.position);
					break;

				case moveType.velocity:
					// Velocity to be used
					Vector2 newVelocity = rbody.velocity/2 + deltaDist * speed;
					newVelocity = Vector2.ClampMagnitude(newVelocity, maxSpeed*10);
					
					// Apply the velocity
					rbody.velocity = newVelocity;
					break;
				}
			}
		}

		CustomUpdate ();
	}

	void FixedUpdate() {
		if (isGrabbed) {
			GrabbedFixedUpdate ();
		}

		// Change layer, but only if player is NOT inside
		if (!isGrabbed || (isGrabbed && isAlt)) {

			//
			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			float radius = 2.56f/2;

			// Check if empty
			// Compares if THIS gameobject is not yet in layer 10
			// TODO: Make 'em switch when they are all empty
			if (gameObject.layer != 10 && !Physics2D.OverlapCircle(pos,radius,playerLayerMask)) {
				// Change layer
				SwapLayer(10);
			}
		}

		CustomFixedUpdate ();
	}
	
	protected virtual void CustomUpdate() {
		// Nofin
	}
	
	protected virtual void CustomFixedUpdate() {
		// Nofin
	}

	public void ParentStartGrab() {
		if (!isGrabbed) {
			isGrabbed = true;

			// Change mass
			rbody.mass = 1f;

			// Change layer
			SwapLayer(11);
			
			// Change color
			ChangeColor(grabColor);

			StartGrab ();
		}
	}

	public void ParentStopGrab() {
		if (isGrabbed) {
			ParentStopAlt();

			isGrabbed = false;
			
			// Change mass
			rbody.mass = originalMass;

			// Change color
			ChangeColor(Color.white);
			
			// Hand
			hand.ResetTarget ();

			// Halter velocity
			rbody.velocity = Vector2.ClampMagnitude(rbody.velocity, maxSpeed);

			StopGrab();
		}
	}


	public void ParentStartAlt() {
		if (!isAlt && enableAlt) {
			isAlt = true;

			// Change color
			ChangeColor(altColor);

			StartAlt ();
		}
	}

	public void ParentStopAlt() {
		if (isAlt && enableAlt) {
			isAlt = false;

			// Change color
			ChangeColor(grabColor);

			StopAlt ();
		}
	}
	

	protected virtual void StartGrab() {
		// Nofin
	}

	protected virtual void StopGrab() {
		// Nofin
	}


	protected virtual void StartAlt() {
		// Nofin
	}
	
	protected virtual void StopAlt() {
		// Nofin
	}


	protected virtual void GrabbedUpdate() {
		// Nofin
	}

	protected virtual void GrabbedFixedUpdate() {
		// Nofin
	}


	public bool GetGrab() {
		return isGrabbed;
	}

	public bool GetAlt() {
		return isAlt;
	}

	public void SwapLayer(int newLayer) {
		if (swapsLayer.Length > 0) {
			foreach (GameObject obj in swapsLayer) {
				obj.layer = newLayer;
			}
		}
	}

	public void ChangeColor(Color color) {
		if (spriteRenderer.Length > 0) {
			foreach (SpriteRenderer renderer in spriteRenderer) {
				renderer.color = color;
			}
		}
	}

}
