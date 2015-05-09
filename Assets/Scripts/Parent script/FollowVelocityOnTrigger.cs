using UnityEngine;
using System.Collections;

public class FollowVelocityOnTrigger: MonoBehaviour {
	
	public Rigidbody2D player;
	public bool active = false;

	// Called from within PlayerMovement
	public void VelocityUpdate (ref Vector2 vel) {
		if (active) {
			// Add my velocity to the player
			vel.x += GetComponent<Rigidbody2D>().velocity.x;
		}
	}
	
	// Activate
	void OnTriggerEnter2D(Collider2D other) {
		if (other.attachedRigidbody == player)
			active = true;
	}
	
	// Deactivate
	void OnTriggerExit2D(Collider2D other) {
		if (other.attachedRigidbody == player)
			active = false;
		
	}
}
